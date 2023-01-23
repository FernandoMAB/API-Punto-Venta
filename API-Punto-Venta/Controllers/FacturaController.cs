using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using API_Punto_Venta.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_Punto_Venta.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FacturaController : ControllerBase
    {
        private readonly ILogger<FacturaController> _logger;
        private readonly IFacturaService _facturaService;

        public FacturaController(ILogger<FacturaController> logger, IFacturaService facturaService)
        {
            _logger = logger;
            _facturaService = facturaService;
        }

        /// <summary>
        /// Consulta de todas las Facturas.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de todas las facturas del sistema.
        /// </remarks>
        /// <returns>Retorna la consulta de todas las facturas del sistema.</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Factura>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogDebug("Start get all Factura Service");
                var facturas = _facturaService.GetAll();
                return Ok(facturas);
            }
            catch (BusinessException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX,ex.InnerException?.Message));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,ex.InnerException?.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,ex.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM,ex.Message).ToString());
            }
        }

        /// <summary>
        /// Consulta de Factura por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de una factura por el Id.
        /// </remarks>
        /// <param name="id"> Código Id de la factura.</param>
        /// <returns>Retorna la consulta de una factura por Id.</returns>
        /// 
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Factura),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                var facturas = _facturaService.GetFactura(id);
                return Ok(facturas);
            }
            catch (BusinessException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX,ex.InnerException?.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,ex.InnerException?.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM,ex.Message).ToString());
            }
        }

        /// <summary>
        /// Realiza el proceso de venta de Productos.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza el proceso de venta de varios productos con facturas detalles y eventos de Kardex.
        /// </remarks>
        /// <param name="factura"> Datos para registrar una nueva factura.</param>
        /// 
        /// <response code="201">Se ha realizado la creación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody, BindRequired] Factura factura)
        {
            try
            {
                var resultado = _facturaService.Save(factura);
                return Created("Creado Exitosamente!", resultado.Result);
            }
            catch (AggregateException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX,ex.InnerException?.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM,ex.Message).ToString());
            }
        }
    }
}
