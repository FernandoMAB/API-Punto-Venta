using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Punto_Venta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Punto_Venta.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if(user != null)
            {
                var token = Generate(user);
                var tokenUsu = new TokenUsu();
                tokenUsu.token = token;
                return Ok(tokenUsu);
            }

            return NotFound("Solicitud Denegada");
        }

        private string Generate(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, user.UsuPNombre),
                new Claim(ClaimTypes.Email, user.UsuEmail),
                new Claim(ClaimTypes.GivenName, user.UsuPNombre),
                new Claim(ClaimTypes.Role, user.UsuEstado),
                new Claim(ClaimTypes.Surname, user.UsuPNombre)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Usuario Authenticate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(o => o.UsuPNombre.ToLower() ==
            userLogin.UserName.ToLower() && o.UsuContrasena == userLogin.Password);

            if (currentUser !=null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
