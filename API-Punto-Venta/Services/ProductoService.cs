using API_Punto_Venta.Context;
using API_Punto_Venta.Dtos;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Punto_Venta.Services;

public class ProductoService : IProductoService
{
    private readonly PuntoVentaContext _context;
    private readonly IMapper _mapper;

    public ProductoService(PuntoVentaContext dbcontext, IMapper mapper)
    {
        _context = dbcontext;
        _mapper = mapper;
    }

    public IEnumerable<Producto> GetAll()
    {
        if (!_context.Productos.Any()) throw new NotFoundException(Constants.NONREGIST);
        var products =  _context.Productos.Include(x => x.CategoriaProductos)
            .ThenInclude(c => c.Cat).ToList();
        foreach (var product in products.Where(product => product.ProPrecio != null))
        {
            product.ProPrecio = Math.Round((double)product.ProPrecio, Constants.ROUND_VAL_2);
        }

        return products.OrderBy(x => x.ProId);
    }

    public ProductDto? GetProducto(int id)
    {
        try
        {
            if (_context.Productos.Where(x => x.ProId == id)
                                    .Include(x => x.Kardices)
                                    .Include(x => x.CategoriaProductos)
                                    .First() is { } product)
            {
                return _mapper.Map<ProductDto>(product);
            }
            throw new NotFoundException(Constants.NONPROD);
        }
        catch (Exception ex)
        {
            throw new NotFoundException(ex.Message);
        }
    }

    public Producto GetProductoByCod(string codBarras)
    {
        return _context.Productos.First(x => x.ProCodBarras != null &&
                                             x.ProCodBarras.Equals(codBarras)) is { } product
                    ? product
                    : throw new NotFoundException(Constants.NONPROD);
    }

    public async Task<IResult> Save(Producto producto)
    {
        if (_context.Productos.FirstOrDefault(x => x.ProCodBarras == producto.ProCodBarras) is {})
        {
            throw new BusinessException(Constants.PRODREPE);
        }


        if (!_context.Productos.Any())
            producto.ProId = 1;
        else
            producto.ProId = _context.Productos.Max(x => x.ProId) + 1;

        producto.FechaIngreso = DateTime.Now;
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return Results.Created($"{producto.ProId}", producto);
    }

    public async Task<IResult> Update(int id, Producto producto)
    {
        if (_context.Productos.FirstOrDefault(x => x.ProCodBarras == producto.ProCodBarras
            && x.ProId != producto.ProId) is {})
        {
            throw new BusinessException(Constants.PRODREPE);
        }
        
        var productUpdate = _context.Productos.Find(id);

        if (productUpdate != null)
        {
            productUpdate.ComId = producto.ComId ?? productUpdate.ComId;
            productUpdate.ProNombre = producto.ProNombre ?? productUpdate.ProNombre;
            productUpdate.ProDescuento = producto.ProDescuento ?? productUpdate.ProDescuento;
            productUpdate.ProPrecio = producto.ProPrecio ?? productUpdate.ProPrecio;
            productUpdate.ProCodBarras = producto.ProCodBarras ?? productUpdate.ProCodBarras;
            productUpdate.ProCategoria = producto.ProCategoria ?? productUpdate.ProCategoria;
            productUpdate.ProMarca = producto.ProMarca ?? productUpdate.ProMarca;
            productUpdate.ProEstIva = producto.ProEstIva ?? productUpdate.ProEstIva;
            productUpdate.ProDetalle = producto.ProDetalle ?? productUpdate.ProDetalle;
            productUpdate.ProEstado = producto.ProEstado ?? productUpdate.ProEstado;
            productUpdate.UsuarioModificacion = producto.UsuarioModificacion;
            productUpdate.FechaModificacion = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return Results.Ok(Constants.UPDATEEX);
        }
        throw new NotFoundException(Constants.NOTFOUND);
    }

    public async Task<IResult> Delete(int id)
    {
        var productToDelete = _context.Productos.Find(id);

        if (productToDelete == null) throw new NotFoundException(Constants.NOTFOUND);
        
        productToDelete.ProEstado = Constants.ESTADO_ELIMINADO;
        await _context.SaveChangesAsync();
        
        return Results.Ok(Constants.DELREGEX);
    }

    public async Task<IResult> AddStock(int id, Producto producto)
    {
        int karId;
        if (!_context.Kardices.Any())
            karId = 1;
        else
            karId = _context.Kardices.Max(x => x.KarId) + 1;

        var productUpdate = _context.Productos.Find(id);

        if (productUpdate == null) throw new NotFoundException(Constants.NOTFOUND);
        if (producto.ProCantidad == null)
            throw new BusinessException(@"Es necesario enviar la cantidad de productos");
        productUpdate.ProStock += producto.ProCantidad;
        producto.ProDetalle = productUpdate.ProDetalle;
        producto.ProId = productUpdate.ProId;
        Kardex kar = ProcesoEntradaKardex(producto, karId);
        productUpdate.ProPrecio = kar.KarBalPrecio;
        await _context.SaveChangesAsync();
        _context.Kardices.Add(kar);
        await _context.SaveChangesAsync();
        return Results.Ok(Constants.UPDATEEX);

    }

    private Kardex ProcesoEntradaKardex(Producto producto, int karId)
    {
        Kardex kardex = _context.Kardices.Where(x => x.ProId == producto.ProId).OrderByDescending(x => x.KarId).First();
        double karEntPreTota = (double)(producto.ProPrecio * producto.ProCantidad);
        int cantidad = (int)producto.ProCantidad;

        return new Kardex
        {
            KarId = karId,
            KarFecha = DateTime.Now,
            KarDetalle = producto.ProDetalle,
            KarSalCantidad = 0,
            KarEntCantidad = cantidad,
            KarSalPrecio = 0,
            KarEntPrecio = producto.ProPrecio,
            KarEntPreTotal = karEntPreTota,
            KarSalPreTotal = 0,
            KarBalCantidad = kardex.KarBalCantidad + cantidad,
            KarBalPrecioTotal = kardex.KarBalPrecioTotal + karEntPreTota,
            KarBalPrecio = (kardex.KarBalPrecioTotal + karEntPreTota) / (kardex.KarBalCantidad + cantidad),
            KarEstado = "V",
            ProId = producto.ProId
        };
    }

}

public interface IProductoService
{
    IEnumerable<Producto> GetAll();
    ProductDto? GetProducto(int id);
    Producto? GetProductoByCod(string codBarras);
    Task<IResult> Save(Producto producto);
    Task<IResult> Update(int id, Producto producto);
    Task<IResult> Delete(int id);
    Task<IResult> AddStock(int id, Producto producto);
}