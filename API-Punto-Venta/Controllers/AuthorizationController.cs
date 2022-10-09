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
        [Authorize(Roles ="1")]
        public IActionResult Admin()
        {
            var currentUser = GetCurrentUser();
            return Ok($"HOLA Admin {currentUser.UserName},{currentUser.EmailAddress} el Rol es: {currentUser.roleId}");
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

                var role = 0;
                if(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value != null)
                {
                    role = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value);
                }
                return new UserLogin
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    roleId = role

                };
            }
            return null;
        }
    }
}