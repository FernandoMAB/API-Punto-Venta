using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.EntityFrameworkCore;
namespace API_Punto_Venta.Services;

public class PermisoService : IPermisoService
{
    private readonly PuntoVentaContext _context;
    public PermisoService(PuntoVentaContext dbcontext)
    {
        _context = dbcontext;
    }

    public IEnumerable<Permiso> GetAll()
    {
        if (_context.Permisos.Any())
            return _context.Permisos.Include(x => x.Rol).Where(x => x.PerEstado != Constants.ESTADO_ELIMINADO);
        throw new NotFoundException("No existen registros");
    }

    public Permiso GetPermiso(int id)
    {
        return _context.Permisos.Find(id)
                is { } permission
                    ? permission
                    : throw new NotFoundException(Constants.NONPERM);
    }

    public IEnumerable<Permiso> GetPermisoByRol(int idRol)
    {
        if (_context.Permisos.Any())
            return _context.Permisos.Where(x => x.RolId == idRol);
        throw new NotFoundException("No existe Permiso con este Rol id");
    }
    public async Task<IResult> Save(Permiso permiso)
    {
        if (!_context.Permisos.Any())
            permiso.PerId = 1;
        else
            permiso.PerId = _context.Permisos.Max(x => x.PerId) + 1;
        
        permiso.FechaIngreso = DateTime.Now;
        _context.Permisos.Add(permiso);
        await _context.SaveChangesAsync();
        return Results.Created($"{permiso.PerId}", permiso.PerId);
    }
    public async Task<IResult> Update(int id, Permiso permiso)
    {
        var permisoToUpdate = _context.Permisos.Find(id);

        if (permisoToUpdate == null) throw new NotFoundException(Constants.NONPERM);
        
        permisoToUpdate.RolId = permiso.RolId;
        permisoToUpdate.PerPantalla = permiso.PerPantalla;
        permisoToUpdate.PerEstado = permiso.PerEstado;
        permisoToUpdate.FechaModificacion = DateTime.Now;
        permisoToUpdate.UsuarioModificacion = permiso.UsuarioModificacion;

        await _context.SaveChangesAsync();
        return Results.Ok(permisoToUpdate);
    }
    public async Task<IResult> Delete(int id)
    {
        if (await _context.Permisos.FindAsync(id) is { } permisoDelete)
        {
            permisoDelete.PerEstado = Constants.ESTADO_ELIMINADO;
            await _context.SaveChangesAsync();
            return Results.Ok(permisoDelete);
        }
        throw new NotFoundException(Constants.NONPERM);
    }
}

public interface IPermisoService
{
    IEnumerable<Permiso> GetAll();
    Permiso GetPermiso(int id);
    IEnumerable<Permiso> GetPermisoByRol(int idRol);
    Task<IResult> Save(Permiso permiso);
    Task<IResult> Update(int id, Permiso permiso);
    Task<IResult> Delete(int id);
}