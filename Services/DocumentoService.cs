using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.EntityFrameworkCore;

namespace API_Punto_Venta.Services
{
    public class DocumentoService : IDocumentoService
    {
        PuntoVentaContext context;
        private readonly ILogger<DocumentoService> logger;

        public DocumentoService(PuntoVentaContext dbcontext, ILogger<DocumentoService> logger)
        {
            context = dbcontext;
            this.logger = logger;
        }

        public IEnumerable<Documento> GetAll()
        {
            if (context.Documentos.Any())
                return context.Documentos;
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Documento? Get(int id)
        {
            return context.Documentos.Find(id)
                is Documento documento
                    ? documento
                    : throw new NotFoundException(Constants.NONDOCU);
        }

        public async Task<IResult> Save(Documento documento)
        {
            context.Documentos.Add(documento);
            await context.SaveChangesAsync();
            return Results.Created($"{documento.DocId}", documento);
        }

        public async Task<IResult> Update(int id, Documento documento)
        {
            var documenToUpdate = context.Documentos.Find(id);

            if (documenToUpdate != null)
            {

                documenToUpdate.DocExtension    = documento.DocExtension;
                documenToUpdate.DocName         = documento.DocName;
                documenToUpdate.DocBase64       = documento.DocBase64;
                documenToUpdate.DocStatus       = documento.DocStatus;
                documenToUpdate.DocIdClient     = documento.DocIdClient;
                documenToUpdate.DocIdUploader   = documento.DocIdUploader;

                await context.SaveChangesAsync();
                return Results.Ok(Constants.UPDATEEX);

            }
            throw new NotFoundException(Constants.NOTFOUND);
        }

        public async Task<IResult> Delete(int id)
        {
            var documenToDelete = context.Documentos.Find(id);

            if (documenToDelete != null)
            {
                context.Documentos.Remove(documenToDelete);
                await context.SaveChangesAsync();
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
