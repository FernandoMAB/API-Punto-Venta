using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Punto_Venta.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ILogger<PermisoController> _logger;
        ICatalogoService catalogoService;

        public CatalogoController(ILogger<PermisoController> logger, ICatalogoService catalogoService)
        {
            _logger = logger;
            this.catalogoService = catalogoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var catalogos = catalogoService.GetAll();
                return Ok(catalogos);
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

        [HttpGet("getByName/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var catalogos = catalogoService.GetByName(name);
                return Ok(catalogos);
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
        public IActionResult Post([FromBody] Catalogo catalogo)
        {
            try
            {
                var resultado = catalogoService.Save(catalogo);
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

        [HttpPatch("Name/{name}/Code/{code}")]
        public IActionResult Patch(string name, string code, [FromBody] Catalogo catalogo)
        {
            try
            {
                var resultado = catalogoService.Update(name, code, catalogo);
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

        [HttpDelete("Name/{name}/Code/{code}")]
        public IActionResult Delete(string name, string code)
        {
            try
            {
                var resultado = catalogoService.Delete(name, code);
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
