using API_Punto_Venta.Context;
using API_Punto_Venta.Models;
using Microsoft.EntityFrameworkCore;

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
        return context.Usuarios.Where(x => x.UsuEstado != Util.Constants.ESTADO_ELIMINADO)
                                .Include(x => (x as Usuario).Rols)
                                .Include(x => x.Cajs)
                                .ToList();
    }
    public Usuario? GetUsuario(int id)
    {
        var usuario = context.Usuarios.Find(id);
        if (usuario != null)
            return usuario;
        return null;
    }

    public async Task<IResult> Save(Usuario usuario){

        if (!context.Usuarios.Any())
            usuario.UsuId = 1;
        else
            usuario.UsuId = context.Usuarios.Max(x => x.UsuId) + 1;
        if(usuario.UsuFecNacimiento == null)
            usuario.UsuFecNacimiento = DateTime.Now.AddYears(-18);
        if(usuario.UsuNumCargas == null)
            usuario.UsuNumCargas = 0;
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();
        return Results.Created($"{usuario.UsuId}", usuario.UsuId);
    }
    public async Task<IResult> Update(int id, Usuario usuario)
    {
        var usuarioUpdate = context.Usuarios.Find(id);

        if(usuarioUpdate != null)
        {
            usuarioUpdate.UsuPNombre        = usuario.UsuPNombre;
            usuarioUpdate.UsuPApellido      = usuario.UsuPApellido;
            usuarioUpdate.UsuSNombre        = usuario.UsuSNombre;
            usuarioUpdate.UsuSApellido      = usuario.UsuSApellido;
            usuarioUpdate.UsuTipoIden       = usuario.UsuTipoIden;
            usuarioUpdate.UsuNumeroIden     = usuario.UsuNumeroIden;
            usuarioUpdate.UsuFecNacimiento  = usuario.UsuFecNacimiento;
            usuarioUpdate.UsuEstCivil       = usuario.UsuEstCivil;
            usuarioUpdate.UsuEmail          = usuario.UsuEmail;
            usuarioUpdate.UsuTelefono       = usuario.UsuTelefono;
            usuarioUpdate.UsuNumCelular     = usuario.UsuNumCelular;
            usuarioUpdate.UsuNumCargas      = usuario.UsuNumCargas;
            usuarioUpdate.UsuEstado         = usuario.UsuEstado;
            usuarioUpdate.UsuUserName       = usuario.UsuUserName;

            await context.SaveChangesAsync();
            return Results.Ok(usuarioUpdate);
        }
        return null;
    }

    public async Task<IResult> Delete(int id)
    {

        if(await context.Usuarios.FindAsync(id) is Usuario usuarioToDelete)
        {
            usuarioToDelete.UsuEstado = Util.Constants.ESTADO_ELIMINADO;
            await context.SaveChangesAsync();
            return Results.Ok(usuarioToDelete);
        }
        return null;
    }
}

public interface IUsuarioService
{
    IEnumerable<Usuario> GetAll();
    Usuario? GetUsuario(int id);
    Task<IResult> Save(Usuario usuario);
    Task<IResult> Update(int id, Usuario usuario);
    Task<IResult> Delete(int id);
}