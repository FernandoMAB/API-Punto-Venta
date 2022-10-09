using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ParametroController: ControllerBase
    {
        private readonly ILogger<ParametroController> _logger;
        IParametroService parametroService;

        public ParametroController(ILogger<ParametroController> logger, IParametroService parametroService)
        {
            _logger = logger;
            this.parametroService = parametroService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try{
                var parametro = parametroService.GetAll();
                if(parametro != null){
                    return Ok(parametro);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpGet("{nem}")]
        public IActionResult GetParametro(string nem)
        {
            try{
                var parametro = parametroService.GetByNemonic(nem);
                if(parametro != null){
                    return Ok(parametro);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Parametrosg parametrosg)
        {
            try{
                var resultado = parametroService.Save(parametrosg);
                var vas = Results.Conflict();
                if(!resultado.Result.ToString().Contains("ConflictObjectResult"))
                    return Ok(resultado.Result);
            }catch(Exception ex){
                return Conflict();
            }
            return Conflict();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Parametrosg parametrosg)
        {
            try{
                var resultado = parametroService.Update(id, parametrosg);
                
                if (resultado.Result.ToString().Contains("ConflictObjectResult"))
                    return Conflict();
                else if (resultado.Result != null)
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
                var resultado = parametroService.Delete(id);
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