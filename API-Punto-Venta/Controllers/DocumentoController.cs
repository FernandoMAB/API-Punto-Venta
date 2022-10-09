using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API_Punto_Venta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly ILogger<DocumentoController> _logger;
        IDocumentoService documentoService;

        public DocumentoController(ILogger<DocumentoController> logger, IDocumentoService documentoService)
        {
            _logger = logger;
            this.documentoService = documentoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var documentos = documentoService.GetAll();
                return Ok(documentos);
            }
            catch (BusinessException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDocumento(int id)
        {
            try
            {
                var documento = documentoService.Get(id);
                return Ok(documento);
            }
            catch (BusinessException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Documento documento)
        {
            try
            {
                var resultado = documentoService.Save(documento);
                return Created("Creado Exitosamente!", resultado.Result);
            }
            catch (AggregateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Documento documento)
        {
            try
            {
                var resultado = documentoService.Update(id, documento);
                return Ok(resultado.Result);
            }
            catch (AggregateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var resultado = documentoService.Delete(id);
                return Ok(resultado.Result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
