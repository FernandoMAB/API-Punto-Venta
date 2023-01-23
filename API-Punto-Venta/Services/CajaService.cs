using API_Punto_Venta.Context;
using API_Punto_Venta.Models;

namespace API_Punto_Venta.Services;

public class CajaService : ICajaService
{
    private readonly PuntoVentaContext _context;

    public CajaService(PuntoVentaContext dbcontext)
    {
        _context = dbcontext;
    }

    public IEnumerable<Caja> GetAll()
    {
        return _context.Cajas;
    }

    public Caja? GetCaja(int id)
    {
        var caja = _context.Cajas.Find(id);
        return caja;
    }

    public async Task Save(Caja caja)
    {
        if (!_context.Cajas.Any())
            caja.CajId = 1;
        else
            caja.CajId = _context.Cajas.Max(x => x.CajId) + 1;

        caja.CajFecha = DateTime.Now;
        caja.FechaIngreso = DateTime.Now;
        

        _context.Add(caja);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, Caja caja)
    {
        var cajaUpdate = _context.Cajas.Find(id);

        if (cajaUpdate != null)
        {
            MappingUpdateCaja1(cajaUpdate, caja);
            MappingUpdateCaja2(cajaUpdate, caja);
            
            await _context.SaveChangesAsync();
        }
    }

    private static void MappingUpdateCaja1(Caja cajaUpdate, Caja caja)
    {
        cajaUpdate.CajMon1C = caja.CajMon1C ?? cajaUpdate.CajMon1C;
        cajaUpdate.CajMon5C = caja.CajMon5C ?? cajaUpdate.CajMon5C;
        cajaUpdate.CajMon10C = caja.CajMon10C ?? cajaUpdate.CajMon10C;
        cajaUpdate.CajMon25C = caja.CajMon25C ?? cajaUpdate.CajMon25C;
        cajaUpdate.CajMon50C = caja.CajMon50C ?? cajaUpdate.CajMon50C;
        cajaUpdate.CajMon1Dol = caja.CajMon1Dol ?? cajaUpdate.CajMon1Dol;
        cajaUpdate.CajBill1Dol = caja.CajBill1Dol ?? cajaUpdate.CajBill1Dol;
        cajaUpdate.FechaModificacion = DateTime.Now;
        cajaUpdate.UsuarioModificacion = caja.UsuarioModificacion;
    }
    
    private static void MappingUpdateCaja2(Caja cajaUpdate, Caja caja)
    {
        cajaUpdate.CajBill5Dol = caja.CajBill5Dol ?? cajaUpdate.CajBill5Dol;
        cajaUpdate.CajBill10Dol = caja.CajBill10Dol ?? cajaUpdate.CajBill10Dol;
        cajaUpdate.CajBill20Dol = caja.CajBill20Dol ?? cajaUpdate.CajBill20Dol;
        cajaUpdate.CajBill50Dol = caja.CajBill50Dol ?? cajaUpdate.CajBill50Dol;
        cajaUpdate.CajBill100Dol = caja.CajBill100Dol ?? cajaUpdate.CajBill100Dol;
        cajaUpdate.CajFecha = DateTime.Now;
        cajaUpdate.CajRegIngreso = caja.CajRegIngreso;
        cajaUpdate.CajRegSalida = caja.CajRegSalida;
        cajaUpdate.CajEstado = caja.CajEstado;
        cajaUpdate.CajTotal = caja.CajTotal;
    }

    public async Task Delete(int id)
    {
        var cajatoToDelete = _context.Cajas.Find(id);

        if (cajatoToDelete != null)
        {
            _context.Remove(cajatoToDelete);
            await _context.SaveChangesAsync();
        }
    }
}

public interface ICajaService
{
    IEnumerable<Caja> GetAll();
    Caja? GetCaja(int id);
    Task Save(Caja caja);
    Task Update(int id, Caja caja);
    Task Delete(int id);
}