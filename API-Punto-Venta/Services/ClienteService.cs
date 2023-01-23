using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;

namespace API_Punto_Venta.Services;

public class ClienteService : IClienteService
{
    readonly PuntoVentaContext _context;

    public ClienteService(PuntoVentaContext dbcontext)
    {
        _context = dbcontext;
    }

    public IEnumerable<Cliente> GetAll()
    {
        if (_context.Clientes.Any())
            return _context.Clientes.OrderBy(x => x.CliId);
        return new List<Cliente>();
    }

    public Cliente GetCliente(int id)
    {
        return _context.Clientes.Find(id) ?? throw new NotFoundException(Constants.NOTFOUND);
    }

    public async Task<IResult> Save(Cliente cliente)
    {
        //Validaciónes
        if (_context.Clientes.FirstOrDefault(x =>
                x.CliTipoIden == cliente.CliTipoIden 
                && x.CliNumeroIden == cliente.CliNumeroIden) is { })
        {
            throw new BusinessException(Constants.CLIEREPE);
        }


        if (!_context.Clientes.Any())
            cliente.CliId = 1;
        else
            cliente.CliId = _context.Clientes.Max(x => x.CliId) + 1;

        cliente.FechaIngreso = DateTime.Now;
        _context.Add(cliente);
        await _context.SaveChangesAsync();
        return Results.Created($"{cliente.CliId}", cliente.CliId);
    }

    public async Task<IResult> Update(int id, Cliente cliente)
    {
        //Validaciónes
        if (_context.Clientes.FirstOrDefault(x =>
                x.CliTipoIden == cliente.CliTipoIden 
                && x.CliNumeroIden == cliente.CliNumeroIden && x.CliId != cliente.CliId) is { })
        {
            throw new BusinessException(Constants.CLIEREPE);
        }
        
        var clienteToUpdate = _context.Clientes.Find(id);

        if (clienteToUpdate == null) throw new NotFoundException(Constants.NOTFOUND);
        
        MappingUpdateProduct1(clienteToUpdate, cliente);
        MappingUpdateProduct2(clienteToUpdate, cliente);

        await _context.SaveChangesAsync();
        return Results.Ok(clienteToUpdate);
    }

    private static void MappingUpdateProduct1(Cliente clienteToUpdate, Cliente cliente)
    {
        clienteToUpdate.CliPNombre = cliente.CliPNombre ?? clienteToUpdate.CliPNombre;
        clienteToUpdate.CliPApellido = cliente.CliPApellido ?? clienteToUpdate.CliPApellido;
        clienteToUpdate.CliSNombre = cliente.CliSNombre ?? clienteToUpdate.CliSNombre;
        clienteToUpdate.CliSApellido = cliente.CliSApellido ?? clienteToUpdate.CliSApellido;
        clienteToUpdate.CliTipoIden = cliente.CliTipoIden ?? clienteToUpdate.CliTipoIden;
        clienteToUpdate.CliNumeroIden = cliente.CliNumeroIden ?? clienteToUpdate.CliNumeroIden;
    }

    private static void MappingUpdateProduct2(Cliente clienteToUpdate, Cliente cliente)
    {
        clienteToUpdate.CliDireccion = cliente.CliDireccion ?? clienteToUpdate.CliDireccion;
        clienteToUpdate.CliEmail = cliente.CliEmail ?? clienteToUpdate.CliEmail;
        clienteToUpdate.CliNumCelular = cliente.CliNumCelular ?? clienteToUpdate.CliNumCelular;
        clienteToUpdate.CliTelefono = cliente.CliTelefono ?? clienteToUpdate.CliTelefono;
        clienteToUpdate.CliEstado = cliente.CliEstado ?? clienteToUpdate.CliEstado;
        clienteToUpdate.FechaModificacion = DateTime.Now;
        clienteToUpdate.UsuarioModificacion = cliente.UsuarioModificacion;

    }

    public async Task<IResult> Delete(int id)
    {
        var clienteToDelete = _context.Clientes.Find(id);

        if (clienteToDelete == null) throw new NotFoundException(Constants.NOTFOUND);
        clienteToDelete.CliEstado = Constants.ESTADO_ELIMINADO;
        await _context.SaveChangesAsync();
        
        return Results.Ok(clienteToDelete);
    }
}

public interface IClienteService
{
    IEnumerable<Cliente> GetAll();
    Cliente GetCliente(int id);
    Task<IResult> Save(Cliente cliente);
    Task<IResult> Update(int id, Cliente cliente);
    Task<IResult> Delete(int id);

}