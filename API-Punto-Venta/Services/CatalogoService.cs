using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.EntityFrameworkCore;


namespace API_Punto_Venta.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly PuntoVentaContext _context;
        private readonly ILogger<CatalogoService> _logger;

        public CatalogoService(PuntoVentaContext dbcontext, ILogger<CatalogoService> logger)
        {
            _context = dbcontext;
            _logger = logger;
        }

        public async Task<IEnumerable<Catalogo>> GetAll()
        {
            _logger.LogDebug("Start service get all catalogs");
            if (_context.Catalogos.Any())
                return await _context.Catalogos.ToListAsync();
            throw new NotFoundException(Constants.NONREGIST);
        }

        public IEnumerable<Catalogo> GetByName(string name)
        {
            return _context.Catalogos.Where(x => x.CataNombre == name)
                .Where(x => x.CataEstado == Constants.ESTADO_VIGENTE) as IEnumerable<Catalogo> 
                   ?? throw new NotFoundException(Constants.NONCATA);
        }

        public async Task<IResult> Save(Catalogo catalogo)
        {
            catalogo.FechaIngreso = DateTime.Now;
            if (_context.Catalogos.FirstOrDefault(x => x.CataNombre == catalogo.CataNombre && x.CataCodigo == catalogo.CataCodigo) is { })
            {
                throw new BusinessException(Constants.CATAREPE);
            }
            _context.Catalogos.Add(catalogo);
            await _context.SaveChangesAsync();
            return Results.Created($"{catalogo.CataNombre}", catalogo);
        }

        public async Task<IResult> Update(int id, Catalogo catalogo)
        {
            var cataToUpdate = _context.Catalogos.Find(id);
            if (_context.Catalogos.FirstOrDefault(x => x.CataNombre == catalogo.CataNombre && x.CataCodigo == catalogo.CataCodigo
                && x.CataId != id) is { })
            {
                throw new BusinessException(Constants.CATAREPE);
            }
            
            if (cataToUpdate != null)
            {

                cataToUpdate.CataNombre = catalogo.CataNombre;
                cataToUpdate.CataCodigo = catalogo.CataCodigo;
                cataToUpdate.CataValor = catalogo.CataValor;
                cataToUpdate.CataEstado = catalogo.CataEstado;
                cataToUpdate.FechaModificacion = DateTime.Now;
                cataToUpdate.UsuarioModificacion = catalogo.UsuarioModificacion;

                await _context.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(int id)
        {
            var cataToDelete = _context.Catalogos.Find(id);

            if (cataToDelete != null)
            {
                _context.Catalogos.Remove(cataToDelete);
                await _context.SaveChangesAsync();
                return Results.Ok(Constants.DELREGEX);
            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

    }

    public interface ICatalogoService
    {
        Task<IEnumerable<Catalogo>> GetAll();
        IEnumerable<Catalogo> GetByName(string name);
        Task<IResult> Save(Catalogo catalogo);
        Task<IResult> Update(int id, Catalogo catalogo);
        Task<IResult> Delete(int id);
    }
}
