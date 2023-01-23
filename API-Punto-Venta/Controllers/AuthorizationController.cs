using System.Security.Claims;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API_Punto_Venta.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// Realiza la consulta del usuario actual.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza la consulta del usuario actual ingresado.
        /// </remarks>
        /// <returns>Retorna la consulta de todos los permisos del sistema.</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        [HttpGet("Admin")]
        [Authorize]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        public IActionResult Admin()
        {
            var currentUser = GetCurrentUser();
            return Ok($"HOLA Admin {currentUser.UserName},{currentUser.EmailAddress} " +
                                       $"el Rol es: {currentUser.RoleId} y Id: {currentUser.Id}");
        }

        /// <summary>
        /// Realiza un saludo a lo visitantes al API.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza un saludo a lo visitantes al API.
        /// </remarks>
        /// <returns>Retorna un mensaje de biendenida</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        [HttpGet("Public")]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        public IActionResult Public()
        {
            return Ok("Hola a todos este es el api de Punto de Venta y Facturación Electrónica!");
        }

        private UserLogin GetCurrentUser(){
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;

                var role = 0;
                var idC = 0;
                if(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value != null)
                {
                    role = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value);
                    idC = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.SerialNumber)?.Value);
                }
                return new UserLogin
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    RoleId = role,
                    Id = idC

                };
            }
            return null;
        }
    }
}