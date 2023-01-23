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
    public class CatalogoController : ControllerBase
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ILogger<CatalogoController> logger, ICatalogoService catalogoService)
        {
            _logger = logger;
            _catalogoService = catalogoService;
        }

        /// <summary>
        /// Consulta de todos los Catálogos.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de todos los catálogos del sistema.
        /// </remarks>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Catalogo>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Catalogo>>> GetAll()
        {
            try
            {
                _logger.LogDebug("Get started get all Catalogs");
                var catalogs = await _catalogoService.GetAll();
                return Ok(catalogs);
            }
            catch (BusinessException ex)
            {
                _logger.LogDebug(ex.StackTrace);
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX,ex.InnerException?.Message));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogDebug(ex.StackTrace);
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,ex.InnerException?.Message));
            }
            catch (NotFoundException ex)
            {
                _logger.LogDebug(ex.StackTrace);
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.StackTrace);
                return Problem(new MessageErrorProblem(Constants.ERRPROBM,ex.Message).ToString());
            }
        }

        /// <summary>
        /// Consulta de Catálogo por nombre.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de un catálogo por el nombre.
        /// </remarks>
        /// <param name="name"> Nombre del catálogo a consultar.</param>
        /// <returns>Retorna la consulta de un catálogo.</returns>
        /// 
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("getByName/{name}")]
        [ProducesResponseType(typeof(Catalogo),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetByName(string name)
        {
            try
            {
                _logger.LogDebug("Get started get Catalog by Name");
                var catalogs = _catalogoService.GetByName(name);
                return Ok(catalogs);
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
        /// Creación de un Catálogo.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza creación de un catálogo.
        /// </remarks>
        /// <param name="catalogo"> Datos para insertar un nuevo catálogo.</param>
        /// 
        /// <response code="201">Se ha realizado la creación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody, BindRequired] Catalogo catalogo)
        {
            try
            {
                _logger.LogDebug("Get started Post Catalog");
                var resultant = _catalogoService.Save(catalogo);
                return Created("Creado Exitosamente!", resultant.Result);
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


        /// <summary>
        /// Modificación de un Catálogo por nombre y código.
        /// </summary>
        /// 
        /// <remarks>
        /// Realiza la modificación de un catálogo por el nombre y código.
        /// </remarks>
        /// <param name="id"> Código del Id de un catálogo.</param>
        /// <param name="catalogo"> Datos para modificar el catálogo.</param>
        /// <returns>Retorna la modificación del registro.</returns>
        /// 
        /// <response code="200">Se ha realizado la modificación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Patch(int id, [FromBody, BindRequired] Catalogo catalogo)
        {
            try
            {
                _logger.LogDebug("Get started Patch Catalog");
                var resultant = _catalogoService.Update(id, catalogo);
                return Ok(resultant.Result);
            }
            catch (AggregateException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX,ex.InnerException?.Message));
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
        /// Eliminación de un Catálogo por nombre y código.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la eliminación de un catálogo por el nombre y código.
        /// </remarks>
        /// <param name="id"> Código del Id de un catálogo.</param>
        /// 
        /// <response code="200">Se ha realizado la eliminación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogDebug("Get started Delete Catalog");
                var resultant = _catalogoService.Delete(id);
                return Ok(resultant.Result);
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

    }
}
