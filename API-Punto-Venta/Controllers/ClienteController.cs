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
    public class ClienteController: ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;

        private readonly IClienteService _clienteService;

        public ClienteController(
            ILogger<ClienteController> logger, IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        /// <summary>
        /// Consulta de todos los Clientes.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de todos los clientes del sistema.
        /// </remarks>
        /// <returns>Retorna la consulta de todos los clientes del sistema.</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Cliente>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try{
                _logger.LogDebug("Start get all clients");
                return Ok(_clienteService.GetAll());
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
        /// Consulta de Cliente por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta de un cliente por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del cliente.</param>
        /// <returns>Retorna la consulta de un cliente por Id.</returns>
        /// 
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cliente),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult GetCliente(int id)
        {
            try
            {
                var result = _clienteService.GetCliente(id);
                return Ok(result);
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
        /// Creación de un Cliente.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza creación de un cliente.
        /// </remarks>
        /// <param name="cliente"> Datos para insertar un nuevo cliente.</param>
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
        public IActionResult Post([FromBody, BindRequired] Cliente cliente)
        {
            try
            {
                var result = _clienteService.Save(cliente);
                return Created(@"Creado Exitosamente!",result.Result);
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
        /// Modificación de un Cliente por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la modificación de un cliente por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del cliente.</param>
        /// <param name="cliente"> Datos para modificar un cliente.</param>
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
        public IActionResult Patch(int id, [FromBody, BindRequired] Cliente cliente)
        {
            try
            {
                var result = _clienteService.Update(id, cliente);
                return Ok(result.Result);
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
        /// Eliminación de un Cliente por Id.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la eliminación de un cliente por el Id.
        /// </remarks>
        /// <param name="id"> Código Id del cliente.</param>
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
            try
            {
                var result =_clienteService.Delete(id);
                return Ok(result.Result);
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
