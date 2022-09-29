using Microsoft.AspNetCore.Mvc;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authorization;
using API_Punto_Venta.Exceptions;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductoController: ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;

        IProductoService productoService;

        public ProductoController(
            ILogger<ProductoController> logger,
            IProductoService productoService)
        {
            _logger = logger;
            this.productoService = productoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(productoService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetProducto(int id)
        {
            var producto = productoService.GetProducto(id);
            if (!Object.ReferenceEquals(producto, null))
                return Ok(producto);
            else 
                return NotFound($"No se encontraron productos con este id : {id}");
        }

        [HttpGet("getByCode/{code}")]
        public IActionResult GetByName(string code)
        {
            try
            {
                var catalogos = productoService.GetProductoByCod(code);
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
        public IActionResult Post([FromBody] Producto producto)
        {
            productoService.Save(producto);
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Producto producto)
        {
            productoService.Update(id, producto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            productoService.Delete(id);
            return Ok();
        }
    }
}