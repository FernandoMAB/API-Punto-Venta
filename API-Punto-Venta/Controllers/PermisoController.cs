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
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class PermisoController: ControllerBase
    {
        private readonly ILogger<PermisoController> _logger;
        private readonly IPermisoService _permisoService;

        public PermisoController(ILogger<PermisoController> logger, IPermisoService permisoService)
        {
            _logger = logger;
            _permisoService = permisoService;
        }

        /// <summary>
        /// Consulta de todos los Permisos.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de todos los permisos del sistema.
        /// </remarks>
        /// <returns>Retorna la consulta de todos los permisos del sistema.</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Permiso>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try{
                _logger.LogDebug("Start get all permisos");
                var permiso = _permisoService.GetAll();
                return Ok(permiso);
            }
            catch (BusinessException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX,ex.Message));
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
        /// Consulta de Permiso por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de un permiso por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del permiso.</param>
        /// <returns>Retorna la consulta de un permiso por Id.</returns>
        /// 
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Permiso),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetPermiso(int id)
        {
            try{
                var permiso = _permisoService.GetPermiso(id);
                return Ok(permiso);
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
        /// Consulta de Permiso por Id del Rol.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de un permiso por el Id del rol.
        /// </remarks>
        /// <param name="id"> Código Id del Rol.</param>
        /// <returns>Retorna la consulta de un permiso por Id del rol.</returns>
        /// 
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("GetByRol/{id}")]
        [ProducesResponseType(typeof(Permiso),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetPermisoByRol(int id)
        {
            try{
                var permiso = _permisoService.GetPermisoByRol(id);
                return Ok(permiso);
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
        /// Creación de un Permiso.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza creación de un permiso.
        /// </remarks>
        /// <param name="permiso"> Datos para insertar un nuevo permiso.</param>
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
        public IActionResult Post([FromBody, BindRequired] Permiso permiso)
        {
            try{
                var resultado = _permisoService.Save(permiso);
                return Ok(resultado.Result);
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
        /// Modificación de un Permiso por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la modificación de un permiso por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del permiso.</param>
        /// <param name="permiso"> Datos para modificar un permiso.</param>
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
        public IActionResult Patch(int id, [FromBody, BindRequired] Permiso permiso)
        {
            try{
                var resultado = _permisoService.Update(id, permiso);
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
        /// Eliminación de un Permiso por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la eliminación de un permiso por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del permiso.</param>
        /// 
        /// <response code="200">Se ha realizado la eliminación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try{
                var resultado = _permisoService.Delete(id);
                if (resultado.Result != null)
                    return Ok(resultado.Result);
                else
                    return NotFound();
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