using System.Data;
using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API_Punto_Venta.Services;

public class UsuarioService : IUsuarioService
{
    private readonly PuntoVentaContext _context;
    public UsuarioService(PuntoVentaContext dbcontext)
    {
        _context = dbcontext;
    }

    public IEnumerable<Usuario> GetAll()
    {
        return _context.Usuarios.Where(x => x.UsuEstado != Constants.ESTADO_ELIMINADO)
                                .Include(x => x.Rols)
                                .Include(x => x.Cajs)
                                .ToList();
    }
    public Usuario GetUsuario(int id)
    {
        return _context.Usuarios.Where(x => x.UsuId == id)
                                .Include(x => x.Rols)
                                .Include(x => x.Cajs).First()
                is { } usuario
                    ? usuario
                    : throw new NotFoundException(Constants.NONUSER);
    }

    public async Task<IResult> Save(Usuario usuario)
    {
        //Validación Usuario Repetido
        if (_context.Usuarios.FirstOrDefault(x => x.UsuUserName == usuario.UsuUserName) is {})
        {
            throw new BusinessException(Constants.USERREPE);
        }
        
        if (!_context.Usuarios.Any())
            usuario.UsuId = 1;
        else
            usuario.UsuId = _context.Usuarios.Max(x => x.UsuId) + 1;
        if (usuario.UsuFecNacimiento == null)
            usuario.UsuFecNacimiento = DateTime.Now.AddYears(-18);

        //Si no viene estado se guarda como Vigente
        usuario.UsuEstado ??= Constants.ESTADO_VIGENTE;


        ICollection<Rol> roles = new List<Rol>();

        if (usuario.Rols.Any())
        {
            foreach (var rol in usuario.Rols)
            {
                try
                {
                    if (_context.Rols.Find(rol.RolId) is { } rolToAdd)
                    {
                        roles.Add(rolToAdd);
                    }
                    else
                    {
                        throw new BusinessException("Rol no existe");
                    }
                }
                catch (Exception ex)
                {
                    throw new BusinessException(ex.Message);
                }
            }
        }
        
        usuario.FechaIngreso = DateTime.Now;
        usuario.Rols = roles;

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return Results.Created($"{usuario.UsuId}", usuario.UsuId);
    }
    public async Task<IResult> Update(int id, Usuario usuario)
    {

        //Validación Usuario Repetido
        if (_context.Usuarios.FirstOrDefault(x => x.UsuUserName == usuario.UsuUserName
            && x.UsuId != id) is {})
        {
            throw new BusinessException(Constants.USERREPE);
        }
        
        var usuarioUpdate = _context.Usuarios.Find(id);

        if (usuarioUpdate != null)
        {
            usuarioUpdate.UsuPNombre = usuario.UsuPNombre ?? usuarioUpdate.UsuPNombre;
            usuarioUpdate.UsuContrasena = usuario.UsuContrasena ?? usuarioUpdate.UsuContrasena;
            usuarioUpdate.UsuPApellido = usuario.UsuPApellido ?? usuarioUpdate.UsuPApellido;
            usuarioUpdate.UsuSNombre = usuario.UsuSNombre ?? usuarioUpdate.UsuSNombre;
            usuarioUpdate.UsuSApellido = usuario.UsuSApellido ?? usuarioUpdate.UsuSApellido;
            usuarioUpdate.UsuTipoIden = usuario.UsuTipoIden ?? usuarioUpdate.UsuTipoIden;
            usuarioUpdate.UsuNumeroIden = usuario.UsuNumeroIden ?? usuarioUpdate.UsuNumeroIden;
            usuarioUpdate.UsuFecNacimiento = usuario.UsuFecNacimiento ?? usuarioUpdate.UsuFecNacimiento;
            usuarioUpdate.UsuEstCivil = usuario.UsuEstCivil ?? usuarioUpdate.UsuEstCivil;
            usuarioUpdate.UsuEmail = usuario.UsuEmail ?? usuarioUpdate.UsuEmail;
            usuarioUpdate.UsuTelefono = usuario.UsuTelefono ?? usuarioUpdate.UsuTelefono;
            usuarioUpdate.UsuNumCelular = usuario.UsuNumCelular ?? usuarioUpdate.UsuNumCelular;
            usuarioUpdate.UsuNumCargas = usuario.UsuNumCargas ?? usuarioUpdate.UsuNumCargas;
            usuarioUpdate.UsuEstado = usuario.UsuEstado ?? usuarioUpdate.UsuEstado;
            usuarioUpdate.UsuUserName = usuario.UsuUserName ?? usuarioUpdate.UsuUserName;
            usuarioUpdate.FechaModificacion = DateTime.Now;
            usuarioUpdate.UsuarioModificacion = usuario.UsuarioModificacion;

            //Si no viene estado se guarda como Vigente
            usuarioUpdate.UsuEstado ??= Constants.ESTADO_VIGENTE;
            
            await _context.SaveChangesAsync();
            await AddAsign(id, usuario);
            return Results.Ok(_context.Usuarios.Where(x => x.UsuId == id)
                .Include(x => x.Rols)
                .Include(x => x.Cajs));
        }
        throw new NotFoundException(Constants.NONUSER);
    }

    public async Task<IResult> AddAsign(int id, Usuario usuario)
    {
        try
        {
            var usuarioUpdate = _context.Usuarios.Find(id);
            if (usuarioUpdate == null) throw new NotFoundException(Constants.NONUSER);

            var paramDel = new []
            {
                // Create parameter(s)    
                new SqlParameter("@i_usuario", SqlDbType.Int){Direction = ParameterDirection.Input, Value = id },
                new SqlParameter("@i_rol", SqlDbType.Int){Direction = ParameterDirection.Input, Value = 0},
                new SqlParameter("@i_caja", SqlDbType.Int){Direction = ParameterDirection.Input, Value = 0},
                new SqlParameter("@i_operacion",SqlDbType.Char){Direction = ParameterDirection.Input, Value = 'D'}
            };
            
            await _context.Usuarios.FromSqlRaw("EXEC sp_asig_usu @i_usuario,@i_rol,@i_caja,@i_operacion", paramDel).ToListAsync();
            foreach (var item in usuario.Rols)
            {
                var parameters = new []
                {
                    // Create parameter(s)
                    new SqlParameter("@i_usuario", SqlDbType.Int){Direction = ParameterDirection.Input, Value = id },
                    new SqlParameter("@i_rol", SqlDbType.Int){Direction = ParameterDirection.Input, Value = item.RolId},
                    new SqlParameter("@i_caja", SqlDbType.Int){Direction = ParameterDirection.Input, Value = 0},
                    new SqlParameter("@i_operacion",SqlDbType.Char){Direction = ParameterDirection.Input, Value = 'I'}
                };
                
                await _context.Usuarios.FromSqlRaw("EXEC sp_asig_usu @i_usuario,@i_rol,@i_caja,@i_operacion", parameters).ToListAsync();
            }
            foreach (var item in usuario.Cajs)
            {
                var parameters = new []
                {
                    // Create parameter(s)
                    new SqlParameter("@i_usuario", SqlDbType.Int){Direction = ParameterDirection.Input, Value = id },
                    new SqlParameter("@i_rol", SqlDbType.Int){Direction = ParameterDirection.Input, Value = 0},
                    new SqlParameter("@i_caja", SqlDbType.Int){Direction = ParameterDirection.Input, Value = item.CajId},
                    new SqlParameter("@i_operacion",SqlDbType.Char){Direction = ParameterDirection.Input, Value = 'I'}
                };
                
                await _context.Usuarios.FromSqlRaw("EXEC sp_asig_usu @i_usuario,@i_rol,@i_caja,@i_operacion", parameters).ToListAsync();
            }
            return Results.Ok(_context.Usuarios.Where(x => x.UsuId == id)
                .Include(x => x.Rols)
                .Include(x => x.Cajs));
        }
        catch(Exception ex)
        {
            throw new BusinessException(ex.Message);
        }
    }

    public async Task<IResult> Delete(int id)
    {

        if (await _context.Usuarios.FindAsync(id) is { } usuarioToDelete)
        {
            usuarioToDelete.UsuEstado = Constants.ESTADO_ELIMINADO;
            await _context.SaveChangesAsync();
            return Results.Ok(usuarioToDelete);
        }
        throw new NotFoundException(Constants.NONUSER);
    }
}

public interface IUsuarioService
{
    IEnumerable<Usuario> GetAll();
    Usuario? GetUsuario(int id);
    Task<IResult> Save(Usuario usuario);
    Task<IResult> Update(int id, Usuario usuario);
    Task<IResult> AddAsign(int id, Usuario usuario);
    Task<IResult> Delete(int id);
}