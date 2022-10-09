using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController: ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;

        IUsuarioService usuarioService;

        public UsuarioController(
            ILogger<UsuarioController> logger,
            IUsuarioService usuarioService)
        {
            _logger = logger;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usu = usuarioService.GetAll();
            foreach (var item in usu)
            {
                item.UsuContrasena = null;
            }
            return Ok(usu);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            try{
                var usuario = usuarioService.GetUsuario(id);
                if(usuario != null){
                    usuario.UsuContrasena = null;
                    return Ok(usuario);
                }
            }catch(Exception ex){
                return Conflict();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try{
                var resultado = usuarioService.Save(usuario);
                return Ok(resultado.Result);
            }catch(Exception ex){
                return Conflict();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Usuario usuario)
        {
            try{
                var resultado = usuarioService.Update(id, usuario);
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
                var resultado = usuarioService.Delete(id);
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