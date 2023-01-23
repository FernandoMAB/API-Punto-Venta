using API_Punto_Venta.Dtos;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using API_Punto_Venta.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Punto_Venta.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KardexController : ControllerBase
    {
        private readonly ILogger<KardexController> _logger;
        private readonly IKardexService _kardexService;

        public KardexController(ILogger<KardexController> logger, IKardexService kardexService)
        {
            _logger = logger;
            _kardexService = kardexService;
        }


        /// <summary>
        /// Consulta de Kardex de productos por Id del producto.
        /// </summary>
        /// <remarks>
        /// Realiza de Kardex de productos por Id del producto.
        /// </remarks>
        /// <param name="id"> Código del Id del producto.</param>
        /// <returns>Retorna la consulta de los kardex relacionados a un producto.</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ICollection<KardexDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogDebug("Start get by id KardexService");
                var kardices = _kardexService.GetKardex(id);
                return Ok(kardices);
            }
            catch (BusinessException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU, ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU, ex.InnerException?.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM,ex.Message).ToString());
            }
        }
    }
}
