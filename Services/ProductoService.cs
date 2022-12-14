using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.EntityFrameworkCore;

namespace API_Punto_Venta.Services;

public class ProductoService : IProductoService
{

    PuntoVentaContext context;

    public ProductoService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Producto> GetAll()
    {
        if (context.Catalogos.Any())
        {
            return context.Productos.Include(x => x.CategoriaProductos).ThenInclude(c => c.Cat);
        }
        else throw new NotFoundException(Constants.NONREGIST);
    }

    public Producto? GetProducto(int id)
    {
        return context.Productos.Find(id)
            is Producto producto
                    ? producto
                    : throw new NotFoundException(Constants.NONPROD);
    }

    public Producto? GetProductoByCod(string codBarras)
    {
        return context.Productos.First(x => x.ProCodBarras.Equals(codBarras)) is Producto producto
                    ? producto
                    : throw new NotFoundException(Constants.NONPROD);
    }

    public async Task<IResult> Save(Producto producto)
    {
        if (!context.Productos.Any())
            producto.ProId = 1;
        else
            producto.ProId = context.Productos.Max(x => x.ProId) + 1;
        
        context.Productos.Add(producto);
        await context.SaveChangesAsync();
        return Results.Created($"{producto.ProId}", producto);
    }

    public async Task<IResult> Update(int id, Producto producto)
    {
        var productUpdate = context.Productos.Find(id);

        if(productUpdate != null)
        {
           productUpdate.ComId          = producto.ComId == null        ? productUpdate.ComId : producto.ComId;
           productUpdate.ProNombre      = producto.ProNombre == null    ? productUpdate.ProNombre : producto.ProNombre;
           productUpdate.ProDescuento   = producto.ProDescuento == null ? productUpdate.ProDescuento : producto.ProDescuento;
           productUpdate.ProPrecio      = producto.ProPrecio == null    ? productUpdate.ProPrecio : producto.ProPrecio;
           productUpdate.ProCodBarras   = producto.ProCodBarras == null ? productUpdate.ProCodBarras : producto.ProCodBarras;
           productUpdate.ProCategoria   = producto.ProCategoria == null ? productUpdate.ProCategoria : producto.ProCategoria;
           productUpdate.ProMarca       = producto.ProMarca == null     ? productUpdate.ProMarca : producto.ProMarca;
           productUpdate.ProEstIva      = producto.ProEstIva == null    ? productUpdate.ProEstIva : producto.ProEstIva;
           productUpdate.ProDetalle     = producto.ProDetalle == null   ? productUpdate.ProDetalle : producto.ProDetalle;
           productUpdate.ProEstado      = producto.ProEstado == null    ? productUpdate.ProEstado : producto.ProEstado;
           productUpdate.ProStock      = producto.ProStock == null    ? productUpdate.ProStock : producto.ProStock;
            await context.SaveChangesAsync();
            return Results.Ok(Constants.UPDATEEX);
        }
        throw new NotFoundException(Constants.NOTFOUND);
    }

    public async Task<IResult> Delete(int id)
    {
        var productoToDelete = context.Productos.Find(id);

        if(productoToDelete != null)
        {
            context.Remove(productoToDelete);
            await context.SaveChangesAsync();
            return Results.Ok(Constants.DELREGEX);
        }
        throw new NotFoundException(Constants.NOTFOUND);
    }
}

public interface IProductoService
{
    IEnumerable<Producto> GetAll();
    Producto? GetProducto(int id);
    Producto? GetProductoByCod(string codBarras);
    Task<IResult> Save(Producto producto);
    Task<IResult> Update(int id, Producto producto);
    Task<IResult> Delete(int id);
}