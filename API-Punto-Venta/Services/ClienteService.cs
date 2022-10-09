using API_Punto_Venta.Context;
using API_Punto_Venta.Models;

namespace API_Punto_Venta.Services;

public class ClienteService : IClienteService
{
    PuntoVentaContext context;

    public ClienteService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Cliente> GetAll()
    {
        return context.Clientes.OrderBy(x => x.CliId);
    }

    public Cliente? GetCliente(int id)
    {
        var cliente = context.Clientes.Find(id);
        return cliente;
    }

    public async Task Save(Cliente cliente)
    {
        if (!context.Clientes.Any())
            cliente.CliId = 1;
        else
            cliente.CliId = context.Clientes.Max(x => x.CliId) + 1;
        
        context.Add(cliente);
        await context.SaveChangesAsync();
    }

    public async Task Update(int id, Cliente cliente)
    {
        var clienteToUpdate = context.Clientes.Find(id);

        if(clienteToUpdate != null)
        {
            clienteToUpdate.CliPNombre      = cliente.CliPNombre == null    ? clienteToUpdate.CliPNombre : cliente.CliPNombre;
            clienteToUpdate.CliPApellido    = cliente.CliPApellido == null  ? clienteToUpdate.CliPApellido : cliente.CliPApellido;
            clienteToUpdate.CliSNombre      = cliente.CliSNombre == null    ? clienteToUpdate.CliSNombre : cliente.CliSNombre;
            clienteToUpdate.CliSApellido    = cliente.CliSApellido == null  ? clienteToUpdate.CliSApellido : cliente.CliSApellido;
            clienteToUpdate.CliTipoIden     = cliente.CliTipoIden == null   ? clienteToUpdate.CliTipoIden : cliente.CliTipoIden;
            clienteToUpdate.CliNumeroIden   = cliente.CliNumeroIden == null ? clienteToUpdate.CliNumeroIden : cliente.CliNumeroIden;
            clienteToUpdate.CliDireccion    = cliente.CliDireccion == null  ? clienteToUpdate.CliDireccion : cliente.CliDireccion;
            clienteToUpdate.CliEmail        = cliente.CliEmail == null      ? clienteToUpdate.CliEmail : cliente.CliEmail;
            clienteToUpdate.CliNumCelular   = cliente.CliNumCelular == null ? clienteToUpdate.CliNumCelular : cliente.CliNumCelular;
            clienteToUpdate.CliTelefono     = cliente.CliTelefono == null   ? clienteToUpdate.CliTelefono : cliente.CliTelefono;
            clienteToUpdate.CliEstado       = cliente.CliEstado == null     ? clienteToUpdate.CliEstado : cliente.CliEstado;
        
            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var clienteToDelete = context.Clientes.Find(id);

        if(clienteToDelete != null)
        {
            context.Remove(clienteToDelete);
            await context.SaveChangesAsync();
        }
    }
}

public interface IClienteService 
{
    IEnumerable<Cliente> GetAll();
    Cliente? GetCliente(int id);
    Task Save(Cliente cliente);
    Task Update(int id, Cliente cliente);
    Task Delete(int id);

}