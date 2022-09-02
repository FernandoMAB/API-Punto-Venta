using API_Punto_Venta.Models;

namespace API_Punto_Venta.Services;

public class CajaService : ICajaService
{

    PuntoVentaContext context;

    public CajaService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Caja> GetAll()
    {
        return context.Cajas;
    }

    public Caja? GetCaja(int id)
    {
        var caja = context.Cajas.Find(id);
        return caja;
    }

    public async Task Save(Caja caja){
        if (!context.Cajas.Any())
            caja.CajId = 1;
        else
            caja.CajId = context.Cajas.Max(x => x.CajId) + 1;
        
        caja.CajFecha = DateTime.Now;

        context.Add(caja);
        await context.SaveChangesAsync();
    }

    public async Task Update(int id, Caja caja)
    {
        var cajaUpdate = context.Cajas.Find(id);

        if(cajaUpdate != null)
        {
           cajaUpdate.CajMon1C      = caja.CajMon1C     == null  ? cajaUpdate.CajMon1C     : caja.CajMon1C;
           cajaUpdate.CajMon5C      = caja.CajMon5C     == null  ? cajaUpdate.CajMon5C     : caja.CajMon5C;
           cajaUpdate.CajMon10C     = caja.CajMon10C    == null  ? cajaUpdate.CajMon10C    : caja.CajMon10C;
           cajaUpdate.CajMon25C     = caja.CajMon25C    == null  ? cajaUpdate.CajMon25C    : caja.CajMon25C;
           cajaUpdate.CajMon50C     = caja.CajMon50C    == null  ? cajaUpdate.CajMon50C    : caja.CajMon50C;
           cajaUpdate.CajMon1Dol    = caja.CajMon1Dol   == null  ? cajaUpdate.CajMon1Dol   : caja.CajMon1Dol;
           cajaUpdate.CajBill1Dol   = caja.CajBill1Dol  == null  ? cajaUpdate.CajBill1Dol  : caja.CajBill1Dol;
           cajaUpdate.CajBill5Dol   = caja.CajBill5Dol  == null  ? cajaUpdate.CajBill5Dol  : caja.CajBill5Dol;
           cajaUpdate.CajBill10Dol  = caja.CajBill10Dol == null  ? cajaUpdate.CajBill10Dol : caja.CajBill10Dol;
           cajaUpdate.CajBill20Dol  = caja.CajBill20Dol == null  ? cajaUpdate.CajBill20Dol : caja.CajBill20Dol;
           cajaUpdate.CajBill50Dol  = caja.CajBill50Dol == null  ? cajaUpdate.CajBill50Dol : caja.CajBill50Dol;
           cajaUpdate.CajBill100Dol = caja.CajBill100Dol== null  ? cajaUpdate.CajBill100Dol: caja.CajBill100Dol;
           cajaUpdate.CajFecha      = DateTime.Now;
           cajaUpdate.CajRegIngreso = caja.CajRegIngreso;
           cajaUpdate.CajRegSalida  = caja.CajRegSalida;
           cajaUpdate.CajEstado     = caja.CajEstado;
           cajaUpdate.CajTotal      = caja.CajTotal;

           await context.SaveChangesAsync(); 
        }
    }

    public async Task Delete(int id)
    {
        var cajatoToDelete = context.Cajas.Find(id);

        if(cajatoToDelete != null)
        {
            context.Remove(cajatoToDelete);
            await context.SaveChangesAsync();
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