using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriaController: ControllerBase
    {
        private readonly ILogger<PermisoController> _logger;
        ICategoriaService categoriaService;

        public CategoriaController(ILogger<PermisoController> logger, ICategoriaService categoriaService)
        {
            _logger = logger;
            this.categoriaService = categoriaService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try{
                var categoria = categoriaService.GetAll();
                if(categoria != null){
                    return Ok(categoria);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetCaregoria(int id)
        {
            try{
                var categoria = categoriaService.GetCategoria(id);
                if(categoria != null){
                    return Ok(categoria);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Categorium categoria)
        {
            try{
                var resultado = categoriaService.Save(categoria);
                return Ok(resultado.Result);
            }catch(Exception ex){
                return Conflict();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Categorium categoria)
        {
            try{
                var resultado = categoriaService.Update(id, categoria);
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
                var resultado = categoriaService.Delete(id);
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