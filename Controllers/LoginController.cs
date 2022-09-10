using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Punto_Venta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace API_Punto_Venta.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private PuntoVentaContext context;

        public LoginController(IConfiguration config, PuntoVentaContext dbcontext)
        {
            _config = config;
            this.context = dbcontext;
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
                tokenUsu.Roles = user.Rols;
                tokenUsu.Cajas = user.Cajs;
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
                new Claim(ClaimTypes.Surname, user.UsuPApellido)
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
            try{
                var currentUser = context.Usuarios.Include(x => x.Rols)
                                                    .Include(x => x.Cajs)
                                                    .FirstOrDefault(o => o.UsuUserName.ToLower() ==
                userLogin.UserName.ToLower() && o.UsuContrasena == userLogin.Password);

                if (currentUser !=null)
                {
                    return currentUser;
                }

                return null;
            }catch(Exception ex){
                return null;
            }
        }
    }
}
