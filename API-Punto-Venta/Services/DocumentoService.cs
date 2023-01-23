using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;

namespace API_Punto_Venta.Services
{
    public class DocumentoService : IDocumentoService
    {
        readonly PuntoVentaContext _context;
        private readonly ILogger<DocumentoService> _logger;

        public DocumentoService(PuntoVentaContext dbcontext, ILogger<DocumentoService> logger)
        {
            _context = dbcontext;
            _logger = logger;
        }

        public IEnumerable<Documento> GetAll()
        {
            _logger.LogDebug("Start service get all Documents");
            if (_context.Documentos.Any())
                return _context.Documentos;
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Documento Get(int id)
        {
            return _context.Documentos.Find(id)
                is { } document
                    ? document
                    : throw new NotFoundException(Constants.NONDOCU);
        }

        public async Task<IResult> Save(Documento documento)
        {
            if (_context.Documentos.FirstOrDefault(x => x.DocName == documento.DocName && x.DocExtension == documento.DocExtension) is {})
            {
                throw new BusinessException(Constants.DOCUREPE);
            }
            
            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();
            return Results.Created($"{documento.DocId}", documento);
        }

        public async Task<IResult> Update(int id, Documento documento)
        {
            if (_context.Documentos.FirstOrDefault(x => x.DocName == documento.DocName && x.DocExtension == documento.DocExtension
                && x.DocId != id) is {})
            {
                throw new BusinessException(Constants.DOCUREPE);
            }
            
            var documenToUpdate = _context.Documentos.Find(id);

            if (documenToUpdate != null)
            {

                documenToUpdate.DocExtension = documento.DocExtension;
                documenToUpdate.DocName = documento.DocName;
                documenToUpdate.DocBase64 = documento.DocBase64;
                documenToUpdate.DocStatus = documento.DocStatus;
                documenToUpdate.DocIdClient = documento.DocIdClient;
                documenToUpdate.DocIdUploader = documento.DocIdUploader;

                await _context.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(int id)
        {
            var documenToDelete = _context.Documentos.Find(id);

            if (documenToDelete != null)
            {
                documenToDelete.DocStatus = Constants.ESTADO_ELIMINADO;
                await _context.SaveChangesAsync();
                return Results.Ok(Constants.DELREGEX);
            }
            throw new NotFoundException(Constants.NOTFOUND);
        }
    }

    public interface IDocumentoService
    {
        IEnumerable<Documento> GetAll();
        Documento? Get(int id);
        Task<IResult> Save(Documento documento);
        Task<IResult> Update(int id, Documento documento);
        Task<IResult> Delete(int id);
    }
}
