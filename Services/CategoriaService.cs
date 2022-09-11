using API_Punto_Venta.Models;
using Microsoft.EntityFrameworkCore;
namespace API_Punto_Venta.Services;

public class CategoriaService : ICategoriaService
{
    PuntoVentaContext context;

    public CategoriaService(PuntoVentaContext dbcontext)
    {
        this.context = dbcontext;
    }

    public IEnumerable<Categorium> GetAll()
    {
        if(context.Permisos.Any())
        return context.Categoria.Where(x => x.CatEstado != Util.Constants.ESTADO_ELIMINADO);
        else return null;
    }
    
    public Categorium? GetCategoria(int id)
    {
        return context.Categoria.Find(id)
                is Categorium cate
                    ? cate
                    : null;
    }
    public async Task<IResult> Save(Categorium categoria)
    {
        if(!context.Categoria.Any())
            categoria.CatId = 1;
        else
            categoria.CatId = context.Categoria.Max(x => x.CatId) + 1;
        context.Categoria.Add(categoria);
        await context.SaveChangesAsync();
        return Results.Created($"{categoria.CatId}",categoria.CatId);
    }

    public async Task<IResult> Update(int id, Categorium categoria)
    {
        var categoriaToUpdate = context.Categoria.Find(id);

        if(categoriaToUpdate != null)
        {
            categoriaToUpdate.CatDescrip = categoria.CatDescrip;
            categoriaToUpdate.CatEstado = categoria.CatEstado;

            await context.SaveChangesAsync();
            return Results.Ok(categoriaToUpdate);
        }
        return null;
    }

    public async Task<IResult> Delete(int id)
    {
        if(await context.Categoria.FindAsync(id) is Categorium categoria)
        {
            categoria.CatEstado = Util.Constants.ESTADO_ELIMINADO;
            await context.SaveChangesAsync();
            return Results.Ok(categoria);
        }
        return null;
    }

}

public interface ICategoriaService
{
    IEnumerable<Categorium> GetAll();
    Categorium? GetCategoria(int id);
    Task<IResult> Save(Categorium categoria);
    Task<IResult> Update(int id, Categorium categoria);
    Task<IResult> Delete(int id);
}