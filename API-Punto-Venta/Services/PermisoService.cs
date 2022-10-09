using API_Punto_Venta.Context;
using API_Punto_Venta.Models;
using Microsoft.EntityFrameworkCore;
namespace API_Punto_Venta.Services;

public class PermisoService : IPermisoService 
{
    PuntoVentaContext context;
    public PermisoService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Permiso> GetAll()
    {
        if(context.Permisos.Any())
        return context.Permisos.Include(x => x.Rol).Where(x => x.PerEstado != Util.Constants.ESTADO_ELIMINADO);
        else return null;
    }

    public Permiso? GetPermiso(int id)
    {
        return context.Permisos.Find(id)
                is Permiso permiso
                    ? permiso
                    : null;
    }

    public IEnumerable<Permiso> GetPermisoByRol(int idRol)
    {
        if(context.Permisos.Any())
        return context.Permisos.Where(x => x.RolId == idRol);
        else return null;
    }
    public async Task<IResult> Save(Permiso permiso)
    {
        if(!context.Permisos.Any())
            permiso.PerId = 1;
        else
            permiso.PerId = context.Permisos.Max(x => x.PerId) + 1;
        context.Permisos.Add(permiso);
        await context.SaveChangesAsync();
        return Results.Created($"{permiso.PerId}",permiso.PerId);
    }
    public async Task<IResult> Update(int id, Permiso permiso)
    {
        var permisoToUpdate = context.Permisos.Find(id);

        if(permisoToUpdate != null)
        {
            permisoToUpdate.RolId = permiso.RolId;
            permisoToUpdate.PerPantalla = permiso.PerPantalla;
            permisoToUpdate.PerEstado = permiso.PerEstado;

            await context.SaveChangesAsync();
            return Results.Ok(permisoToUpdate);
        }
        return null;
    }
    public async Task<IResult> Delete(int id)
    {
        if(await context.Permisos.FindAsync(id) is Permiso permisoDelete)
        {
            permisoDelete.PerEstado = Util.Constants.ESTADO_ELIMINADO;
            await context.SaveChangesAsync();
            return Results.Ok(permisoDelete);
        }
        return null;
    }
}

public interface IPermisoService
{
    IEnumerable<Permiso> GetAll();
    Permiso? GetPermiso(int id);
    IEnumerable<Permiso> GetPermisoByRol(int idRol);
    Task<IResult> Save(Permiso permiso);
    Task<IResult> Update(int id, Permiso permiso);
    Task<IResult> Delete(int id);
}