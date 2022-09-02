using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CajaController: ControllerBase
    {
        private readonly ILogger<CajaController> _logger;

        ICajaService cajaService;

        public CajaController(
            ILogger<CajaController> logger,
            ICajaService cajaService)
        {
            _logger = logger;
            this.cajaService = cajaService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(cajaService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCaja(int id)
        {
            var caja = cajaService.GetCaja(id);
            if (!Object.ReferenceEquals(caja, null))
                return Ok(caja);
            else 
                return NotFound($"No se encontraron cajas con este id : {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Caja caja)
        {
            cajaService.Save(caja);
            return Ok();
        }
        
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Caja caja)
        {
            cajaService.Update(id, caja);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            cajaService.Delete(id);
            return Ok();
        }
    }
}
