using API_Punto_Venta.Exceptions;
using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using API_Punto_Venta.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CajaController: ControllerBase
    {
        private readonly ILogger<CajaController> _logger;

        private readonly ICajaService _cajaService;

        public CajaController(ILogger<CajaController> logger, ICajaService cajaService)
        {
            _logger = logger;
            _cajaService = cajaService;
        }

        /// <summary>
        /// Consulta de todas las cajas.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de todas las cajas del sistema.
        /// </remarks>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Caja>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogDebug("Start get all cajas");
                return Ok(_cajaService.GetAll());
            }
            catch (BusinessException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX, ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU, ex.InnerException?.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU, ex.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM, ex.Message).ToString());
            }
        }


        /// <summary>
        /// Consulta de Caja por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de una caja por el Id.
        /// </remarks>
        /// <param name="id"> Código Id de la caja.</param>
        /// <returns>Retorna la consulta de una caja por Id.</returns>
        /// 
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Caja), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem), StatusCodes.Status500InternalServerError)]
        public IActionResult GetCaja(int id)
        {
            var caja = _cajaService.GetCaja(id);
            if (!Object.ReferenceEquals(caja, null))
                return Ok(caja);
            else 
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,$"No se encontraron cajas con este id : {id}"));
        }

        /// <summary>
        /// Creación de una Caja.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza creación de una caja.
        /// </remarks>
        /// <param name="caja"> Datos para insertar una nueva caja.</param>
        /// 
        /// <response code="201">Se ha realizado la creación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorConflict), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody, BindRequired] Caja caja)
        {
            try
            {
                _cajaService.Save(caja);
                return Ok();
            }
            catch (AggregateException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX, ex.InnerException?.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM, ex.Message).ToString());
            }
        }

        /// <summary>
        /// Modificación de una Caja por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la modificación de una caja por el Id.
        /// </remarks>
        /// <param name="id"> Código Id de la caja.</param>
        /// <param name="caja"> Datos para modificar una caja.</param>
        /// 
        /// <response code="200">Se ha realizado la modificación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem), StatusCodes.Status500InternalServerError)]
        public IActionResult Patch(int id, [FromBody, BindRequired] Caja caja)
        {
            try
            {
                _cajaService.Update(id, caja);
                return Ok();
            }
            catch(AggregateException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX, ex.InnerException?.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU, ex.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM, ex.Message).ToString());
            }
        }

        /// <summary>
        /// Eliminación de un Caja por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la eliminación de una caja por el Id.
        /// </remarks>
        /// <param name="id"> Código Id de la caja.</param>
        /// 
        /// <response code="200">Se ha realizado la eliminación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorProblem), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                _cajaService.Delete(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU, ex.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM, ex.Message).ToString());
            }
        }
    }
}
