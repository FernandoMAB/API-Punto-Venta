using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolController: ControllerBase
    {

        private readonly ILogger<RolController> _logger;
        IRolService rolService;

        public RolController(
            ILogger<RolController> logger,
            IRolService rolService)
        {
            _logger = logger;
            this.rolService = rolService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(rolService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetRol(int id)
        {
            try{
                var rol = rolService.GetRol(id);
                if(rol != null){
                    return Ok(rol);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rol rol)
        {
            try{
                var resultado = rolService.Save(rol);
                return Ok(resultado.Result);
            }catch(Exception ex){
                return Conflict();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Rol rol)
        {
            try{
                var resultado = rolService.Update(id, rol);
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
                var resultado = rolService.Delete(id);
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