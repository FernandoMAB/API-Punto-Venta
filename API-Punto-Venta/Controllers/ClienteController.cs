using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController: ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;

        IClienteService clienteService;

        public ClienteController(
            ILogger<ClienteController> logger,
            IClienteService clienteService)
        {
            _logger = logger;
            this.clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(clienteService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCliente(int id)
        {
            var cliente = clienteService.GetCliente(id);
            if (!Object.ReferenceEquals(cliente, null))
                return Ok(cliente);
            else 
                return NotFound($"No se encontraron clientes con este id : {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            clienteService.Save(cliente);
            return Ok();
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Cliente cliente)
        {
            clienteService.Update(id, cliente);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            clienteService.Delete(id);
            return Ok();
        }
    }
}
