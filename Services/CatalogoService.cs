using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace API_Punto_Venta.Services
{
    public class CatalogoService : ICatalogoService
    {
        PuntoVentaContext context;
        private readonly ILogger<CatalogoService> logger;

        public CatalogoService (PuntoVentaContext dbcontext, ILogger<CatalogoService> logger)
        {
            context = dbcontext;
            this.logger = logger;
        }

        public IEnumerable<Catalogo> GetAll()
        {
            if (context.Catalogos.Any())
                return context.Catalogos;
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public IEnumerable<Catalogo>? GetByName(string name)
        {
            return context.Catalogos.Where(x => x.CataNombre == name)
                                    .Where(x => x.CataEstado == Constants.ESTADO_VIGENTE)
                is IEnumerable<Catalogo> catalogo
                    ? catalogo
                    : throw new NotFoundException(Constants.NONCATA);
        }

        public async Task<IResult> Save(Catalogo catalogo)
        {
            context.Catalogos.Add(catalogo);
            await context.SaveChangesAsync();
            return Results.Created($"{catalogo.CataNombre}", catalogo);
        }

        public async Task<IResult> Update(string name, string code, Catalogo catalogo)
        {
            var cataToUpdate = context.Catalogos.First(x => x.CataNombre.Equals(name) && x.CataCodigo.Equals(code));

            if (cataToUpdate != null)
            {

                cataToUpdate.CataNombre = catalogo.CataNombre;
                cataToUpdate.CataCodigo = catalogo.CataCodigo;
                cataToUpdate.CataValor  = catalogo.CataValor;
                cataToUpdate.CataEstado = catalogo.CataEstado;

                await context.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(string name, string code)
        {
            var cataToDelete = context.Catalogos.First(x => x.CataNombre.Equals(name) && x.CataCodigo.Equals(code));

            if (cataToDelete != null)
            {
                context.Catalogos.Remove(cataToDelete);
                await context.SaveChangesAsync();
                return Results.Ok(Constants.DELREGEX);
            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

    }

    public interface ICatalogoService
    {
        IEnumerable<Catalogo> GetAll();
        IEnumerable<Catalogo>? GetByName(string name);
        Task<IResult> Save(Catalogo catalogo);
        Task<IResult> Update(string name, string code, Catalogo catalogo);
        Task<IResult> Delete(string name, string code);
    }
}
