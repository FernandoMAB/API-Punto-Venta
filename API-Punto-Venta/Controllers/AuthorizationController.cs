using System.Security.Claims;
using API_Punto_Venta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API_Punto_Venta.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        [HttpGet("Admin")]
        [Authorize(Roles = "E")]
        public IActionResult Admin()
        {
            var currentUser = GetCurrentUser();
            return Ok($"HOLA Admin {currentUser.UserName},{currentUser.EmailAddress}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("HOLA");
        }

        private UserLogin GetCurrentUser(){
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new UserLogin
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}