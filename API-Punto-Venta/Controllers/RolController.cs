using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Util;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class RolController: ControllerBase
    {

        private readonly ILogger<RolController> _logger;
        private readonly IRolService _rolService;

        public RolController(
            ILogger<RolController> logger,
            IRolService rolService)
        {
            _logger = logger;
            _rolService = rolService;
        }

        /// <summary>
        /// Consulta de todas los roles.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de todos los roles del sistema.
        /// </remarks>
        /// <returns>Retorna la consulta de todos los roles del sistema.</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Rol>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogDebug("Star get all roles");
                return Ok(_rolService.GetAll());
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
        /// Consulta de Rol por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de un rol por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del rol.</param>
        /// <returns>Retorna la consulta de un rol por Id.</returns>
        /// 
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Rol),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetRol(int id)
        {
            try{
                var rol = _rolService.GetRol(id);
                if(rol != null){
                    return Ok(rol);
                }
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
            return NotFound();
        }

        /// <summary>
        /// Creación de un Rol.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza creación de un rol.
        /// </remarks>
        /// <param name="rol"> Datos para insertar un nuevo rol.</param>
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
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody, BindRequired] Rol rol)
        {
            try{
                var resultado = _rolService.Save(rol);
                return Created(@"Creado Exitosamente!",resultado.Result);
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
        /// Modificación de una Rol por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la modificación de un rol por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del rol.</param>
        /// <param name="rol"> Datos para insertar un nuevo rol.</param>
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
        public IActionResult Patch(int id, [FromBody, BindRequired] Rol rol)
        {
            try{
                var resultado = _rolService.Update(id, rol);
                if (resultado.Result != null)
                    return Ok(resultado.Result);
                else
                    return NotFound();
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
        /// Eliminación de un Rol por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la eliminación de un rol por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del rol.</param>
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
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var resultado = _rolService.Delete(id);
                return Ok(resultado.Result);
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