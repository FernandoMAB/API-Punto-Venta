using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PermisoController: ControllerBase
    {
        private readonly ILogger<PermisoController> _logger;
        IPermisoService permisoService;

        public PermisoController(ILogger<PermisoController> logger, IPermisoService permisoService)
        {
            _logger = logger;
            this.permisoService = permisoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try{
                var permiso = permisoService.GetAll();
                if(permiso != null){
                    return Ok(permiso);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetPermiso(int id)
        {
            try{
                var permiso = permisoService.GetPermiso(id);
                if(permiso != null){
                    return Ok(permiso);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Permiso permiso)
        {
            try{
                var resultado = permisoService.Save(permiso);
                return Ok(resultado.Result);
            }catch(Exception ex){
                return Conflict();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Permiso permiso)
        {
            try{
                var resultado = permisoService.Update(id, permiso);
                if (resultado.Result != null)
                    return Ok(resultado.Result);
                else
                    return NotFound();
            }catch(Exception ex){
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try{
                var resultado = permisoService.Delete(id);
                if (resultado.Result != null)
                    return Ok(resultado.Result);
                else
                    return NotFound();
            }catch(Exception ex){
                return Conflict();
            }
        }

    }
}