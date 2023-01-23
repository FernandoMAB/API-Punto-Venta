using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;

namespace API_Punto_Venta.Services;

public class CategoriaService : ICategoriaService
{
    private readonly PuntoVentaContext _context;

    public CategoriaService(PuntoVentaContext dbcontext)
    {
        _context = dbcontext;
    }

    public IEnumerable<Categorium> GetAll()
    {
        if (_context.Categoria.Any())
            return _context.Categoria.Where(x => x.CatEstado != Constants.ESTADO_ELIMINADO);
        return new List<Categorium>();
    }

    public Categorium GetCategoria(int id)
    {
        return _context.Categoria.Find(id)
                is { } cate
                    ? cate
                    : throw new NotFoundException(Constants.NOTFOUND);
    }
    public async Task<IResult> Save(Categorium categoria)
    {
        if (!_context.Categoria.Any())
            categoria.CatId = 1;
        else
            categoria.CatId = _context.Categoria.Max(x => x.CatId) + 1;
        _context.Categoria.Add(categoria);
        await _context.SaveChangesAsync();
        return Results.Created($"{categoria.CatId}", categoria.CatId);
    }

    public async Task<IResult> Update(int id, Categorium categoria)
    {
        var categoriaToUpdate = _context.Categoria.Find(id);

        if (categoriaToUpdate == null) throw new NotFoundException(Constants.NOTFOUND);
        categoriaToUpdate.CatDescrip = categoria.CatDescrip;
        categoriaToUpdate.CatEstado = categoria.CatEstado;

        await _context.SaveChangesAsync();
        return Results.Ok(categoriaToUpdate);
    }

    public async Task<IResult> Delete(int id)
    {
        if (await _context.Categoria.FindAsync(id) is not { } categoria)
            throw new NotFoundException(Constants.NOTFOUND);
        categoria.CatEstado = Constants.ESTADO_ELIMINADO;
        await _context.SaveChangesAsync();
        return Results.Ok(categoria);
    }

}

public interface ICategoriaService
{
    IEnumerable<Categorium> GetAll();
    Categorium GetCategoria(int id);
    Task<IResult> Save(Categorium categoria);
    Task<IResult> Update(int id, Categorium categoria);
    Task<IResult> Delete(int id);
}