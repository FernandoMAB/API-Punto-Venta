using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;

namespace API_Punto_Venta.Services;

public class RolService : IRolService
{
    private readonly PuntoVentaContext _context;

    public RolService(PuntoVentaContext dbcontext)
    {
        _context = dbcontext;
    }

    public IEnumerable<Rol> GetAll()
    {
        return _context.Rols.Where(x => x.RolEstado != Constants.ESTADO_ELIMINADO).OrderBy(x => x.RolId);
    }

    public Rol? GetRol(int id)
    {
        return _context.Rols.Find(id)
                is { } model
                    ? model
                    : null;
    }

    public async Task<IResult> Save(Rol rol)
    {
        //Validación de Repetido
        if (_context.Rols.FirstOrDefault(x => x.RolDescrip == rol.RolDescrip) is {})
        {
            throw new BusinessException(Constants.ROLREPE);
        }

        if (!_context.Rols.Any())
            rol.RolId = 1;
        else
            rol.RolId = _context.Rols.Max(x => x.RolId) + 1;
        
        rol.FechaIngreso = DateTime.Now;
        _context.Rols.Add(rol);
        await _context.SaveChangesAsync();
        return Results.Created($"{rol.RolId}", rol.RolId);
    }

    public async Task<IResult> Update(int id, Rol rol)
    {
        //Validación de Repetido
        if (_context.Rols.FirstOrDefault(x => x.RolDescrip == rol.RolDescrip
            && x.RolId != id) is {})
        {
            throw new BusinessException(Constants.ROLREPE);
        }
        
        var rolUpdate = _context.Rols.Find(id);

        if (rolUpdate != null)
        {
            rolUpdate.RolDescrip = rol.RolDescrip;
            rolUpdate.Permisos = rol.Permisos;
            rolUpdate.RolEstado = rol.RolEstado;
            rolUpdate.FechaModificacion = DateTime.Now;
            rolUpdate.UsuarioModificacion = rol.UsuarioModificacion;

            await _context.SaveChangesAsync();
            return Results.Ok(rolUpdate);
        }
        throw new NotFoundException(Constants.NONEROL);
    }

    public async Task<IResult> Delete(int id)
    {

        if (await _context.Rols.FindAsync(id) is { } rolToDelete)
        {
            rolToDelete.RolEstado = Constants.ESTADO_ELIMINADO;
            await _context.SaveChangesAsync();
            return Results.Ok(rolToDelete);
        }
        throw new NotFoundException(Constants.NONEROL);
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