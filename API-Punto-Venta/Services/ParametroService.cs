using API_Punto_Venta.Context;
using API_Punto_Venta.Models;
using Microsoft.EntityFrameworkCore;
namespace API_Punto_Venta.Services;

public class ParametroService : IParametroService
{
    PuntoVentaContext context;

    public ParametroService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Parametrosg> GetAll()
    {
        if(context.Parametrosgs.Any())
        return context.Parametrosgs.Where(x => x.ParEstado != Util.Constants.ESTADO_ELIMINADO);
        else return null;
    }

    public Parametrosg? GetByNemonic(string nem)
    {
        return context.Parametrosgs.FirstOrDefault( x => x.ParNemonico.Equals(nem))
                is Parametrosg par
                    ? par
                    : null;
    }

    public async Task<IResult> Save(Parametrosg parametrosg)
    {
        if(!context.Parametrosgs.Any())
            parametrosg.ParId = 1;
        else
            parametrosg.ParId = context.Parametrosgs.Max(x => x.ParId) + 1;
        
        if(context.Parametrosgs.FirstOrDefault(x => x.ParNemonico.Equals(parametrosg.ParNemonico)) 
                                                                    is Parametrosg paraRepetido)
            return Results.Conflict();

        context.Parametrosgs.Add(parametrosg);
        await context.SaveChangesAsync();
        return Results.Created($"{parametrosg.ParId}",parametrosg.ParId);
    }

    public async Task<IResult> Update(int id, Parametrosg parametrosg)
    {
        var parametroToUpdate = context.Parametrosgs.Find(id);

        if(parametroToUpdate != null)
        {
            parametroToUpdate.ParDescrip    = parametrosg.ParDescrip;
            parametroToUpdate.ParEstado     = parametrosg.ParEstado;
            parametroToUpdate.ParNemonico   = parametrosg.ParNemonico == null ? parametroToUpdate.ParNemonico : parametrosg.ParNemonico;
            parametroToUpdate.ParValor      = parametrosg.ParValor;

            if(context.Parametrosgs.FirstOrDefault
                    (x => x.ParNemonico.Equals(parametrosg.ParNemonico) && x.ParId != id) 
                        is Parametrosg paraRepetido)
                return Results.Conflict();

            await context.SaveChangesAsync();
            return Results.Ok(parametroToUpdate);
        }
        return null;
    }

    public async Task<IResult> Delete(int id)
    {
        if(await context.Parametrosgs.FindAsync(id) is Parametrosg parametrosg)
        {
            parametrosg.ParEstado = Util.Constants.ESTADO_ELIMINADO;
            await context.SaveChangesAsync();
            return Results.Ok(parametrosg);
        }
        return null;
    }

}

public interface IParametroService
{
    IEnumerable<Parametrosg> GetAll();
    Parametrosg? GetByNemonic(string nem);
    Task<IResult> Save(Parametrosg parametrosg);
    Task<IResult> Update(int id, Parametrosg parametrosg);
    Task<IResult> Delete(int id);
    
}