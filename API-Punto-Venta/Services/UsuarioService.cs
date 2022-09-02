using API_Punto_Venta.Models;

namespace API_Punto_Venta.Services;

public class UsuarioService : IUsuarioService
{
    PuntoVentaContext context;
    public UsuarioService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Usuario> GetAll()
    {
        return context.Usuarios;
    }
    public Usuario? GetUsuario(int id)
    {
        var usuario = context.Usuarios.Find(id);
        return usuario;
    }

    public async Task Save(Usuario usuario){

        if (!context.Usuarios.Any())
            usuario.UsuId = 1;
        else
            usuario.UsuId = context.Usuarios.Max(x => x.UsuId) + 1;
        
        context.Usuarios.Add(usuario);
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
}

public interface IUsuarioService
{
    IEnumerable<Usuario> GetAll();
    Usuario? GetUsuario(int id);
    Task Save(Usuario usuario);
    //Task Update(int id, Usuario usuario);
    //Task Delete(int id);
}