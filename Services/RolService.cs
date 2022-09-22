using API_Punto_Venta.Context;
using API_Punto_Venta.Models;

namespace API_Punto_Venta.Services;

public class RolService : IRolService
{
    PuntoVentaContext context;

    public RolService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Rol> GetAll()
    {
        return context.Rols.Where(x => x.RolEstado != Util.Constants.ESTADO_ELIMINADO).OrderBy(x => x.RolId);
    }

    public Rol? GetRol(int id)
    {
        return context.Rols.Find(id)
                is Rol model
                    ? model
                    : null;
    }

    public async Task<IResult> Save(Rol rol){

        if (!context.Rols.Any())
            rol.RolId = 1;
        else
            rol.RolId = context.Rols.Max(x => x.RolId) + 1;
        context.Rols.Add(rol);
        await context.SaveChangesAsync();
        return Results.Created($"{rol.RolId}", rol.RolId);
    }

    public async Task<IResult> Update(int id, Rol rol)
    {
        var rolUpdate = context.Rols.Find(id);

        if(rolUpdate != null)
        {
            rolUpdate.RolDescrip        = rol.RolDescrip;
            rolUpdate.Permisos          = rol.Permisos;
            rolUpdate.RolEstado         = rol.RolEstado;

            await context.SaveChangesAsync();
            return Results.Ok(rolUpdate);
        }
        return null;
    }

    public async Task<IResult> Delete(int id)
    {

        if(await context.Rols.FindAsync(id) is Rol rolToDelete)
        {
            rolToDelete.RolEstado = Util.Constants.ESTADO_ELIMINADO;
            await context.SaveChangesAsync();
            return Results.Ok(rolToDelete);
        }
        return null;
    }
}

public interface IRolService
{
    IEnumerable<Rol> GetAll();
    Rol? GetRol(int id);
    Task<IResult> Save(Rol rol);
    Task<IResult> Update(int id, Rol rol);
    Task<IResult> Delete(int id);
}