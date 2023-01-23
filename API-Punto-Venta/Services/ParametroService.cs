using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;

namespace API_Punto_Venta.Services;

public class ParametroService : IParametroService
{
    readonly PuntoVentaContext _context;
    private const string FMT = "000000000.##";

    public ParametroService(PuntoVentaContext dbcontext)
    {
        this._context = dbcontext;
    }

    public IEnumerable<Parametrosg> GetAll()
    {
        if (_context.Parametrosgs.Any())
            return _context.Parametrosgs.Where(x => x.ParEstado != Constants.ESTADO_ELIMINADO);
        throw new NotFoundException(Constants.NOTFOUND);
    }

    public Parametrosg GetByNemonic(string nem)
    {
        if (_context.Parametrosgs.FirstOrDefault(x => x.ParNemonico != null && x.ParNemonico.Equals(nem)) is { } par)
        {
            if (par.ParNemonico != null && par.ParNemonico.Equals("SECU"))
            {
                if (par.ParValor != null)
                {
                    int x = int.Parse(par.ParValor);
                    x += 1;
                    var formatted = x.ToString(FMT);
                    par.ParValor = formatted;
                }

                _context.SaveChanges();
            }
            return par;
        }
        throw new NotFoundException(Constants.NOTFOUND);
    }

    public async Task<IResult> Save(Parametrosg parametrosg)
    {
        if (!_context.Parametrosgs.Any())
            parametrosg.ParId = 1;
        else
            parametrosg.ParId = _context.Parametrosgs.Max(x => x.ParId) + 1;

        if (_context.Parametrosgs.FirstOrDefault(x => x.ParNemonico != null &&
                                                      x.ParNemonico.Equals(parametrosg.ParNemonico))
            is { })
            throw new BusinessException(Constants.NEMREPE);

        parametrosg.FechaIngreso = DateTime.Now;
        _context.Parametrosgs.Add(parametrosg);
        await _context.SaveChangesAsync();
        return Results.Created($"{parametrosg.ParId}", parametrosg.ParId);
    }

    public async Task<IResult> Update(int id, Parametrosg parametrosg)
    {
        var parametroToUpdate = _context.Parametrosgs.Find(id);

        if (parametroToUpdate != null)
        {
            parametroToUpdate.ParDescrip = parametrosg.ParDescrip;
            parametroToUpdate.ParEstado = parametrosg.ParEstado;
            parametroToUpdate.ParNemonico = parametrosg.ParNemonico ?? parametroToUpdate.ParNemonico;
            parametroToUpdate.ParValor = parametrosg.ParValor;
            parametroToUpdate.FechaModificacion = DateTime.Now;
            parametroToUpdate.UsuarioModificacion = parametrosg.UsuarioModificacion;

            if (_context.Parametrosgs.FirstOrDefault
                    (x => x.ParNemonico != null &&
                          x.ParNemonico.Equals(parametrosg.ParNemonico) && x.ParId != id)
                        is { })
                throw new BusinessException(Constants.NEMREPE);

            await _context.SaveChangesAsync();
            return Results.Ok(parametroToUpdate);
        }
        throw new NotFoundException(Constants.NOTFOUND);
    }

    public async Task<IResult> Delete(int id)
    {
        if (await _context.Parametrosgs.FindAsync(id) is { } parametrise)
        {
            parametrise.ParEstado = Constants.ESTADO_ELIMINADO;
            await _context.SaveChangesAsync();
            return Results.Ok(parametrise);
        }
        throw new NotFoundException(Constants.NOTFOUND);
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