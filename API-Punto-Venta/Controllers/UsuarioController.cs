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
            return Ok(usuarioService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            return Ok(usuarioService.GetUsuario(id));
        }

    }
}