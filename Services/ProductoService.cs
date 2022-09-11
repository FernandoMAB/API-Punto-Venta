using API_Punto_Venta.Models;
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
        return context.Productos.Include(x => x.CategoriaProductos).ThenInclude(c => c.Cat);
    }

    public Producto? GetProducto(int id)
    {
        var producto = context.Productos.Find(id);
        return producto;
    }

    public async Task Save(Producto producto){

        if (!context.Productos.Any())
            producto.ProId = 1;
        else
            producto.ProId = context.Productos.Max(x => x.ProId) + 1;
        
        context.Productos.Add(producto);
        await context.SaveChangesAsync();
    }

    public async Task Update(int id, Producto producto)
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
           await context.SaveChangesAsync(); 
        }
    }

    public async Task Delete(int id)
    {
        var productoToDelete = context.Productos.Find(id);

        if(productoToDelete != null)
        {
            context.Remove(productoToDelete);
            await context.SaveChangesAsync();
        }
    }
}

public interface IProductoService
{
    IEnumerable<Producto> GetAll();
    Producto? GetProducto(int id);
    Task Save(Producto producto);
    Task Update(int id, Producto producto);
    Task Delete(int id);
}