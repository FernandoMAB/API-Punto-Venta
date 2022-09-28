using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Punto_Venta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Util;
using API_Punto_Venta.Services;

namespace API_Punto_Venta.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private PuntoVentaContext context;
        IPermisoService permisoService;


        public LoginController(IConfiguration config, PuntoVentaContext dbcontext, IPermisoService permisoService)
        {
            _config = config;
            this.context = dbcontext;
            this.permisoService = permisoService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if(user != null)
            {
                var token = GenerateInicial(user);
                var tokenUsu = new TokenUsu();
                tokenUsu.token = token;
                tokenUsu.Roles = user.Rols;
                tokenUsu.Cajas = user.Cajs;
                return Ok(tokenUsu);
            }

            return NotFound("Solicitud Denegada");
        }

        [Authorize]
        [HttpPost("SelectRole")]
        public IActionResult SelectRole([FromBody] UserLogin userLogin)
        {
            try
            {
                if (userLogin.roleId == 0)
                {
                    return BadRequest();
                }
                var user = Authenticate(userLogin);

                if (user != null)
                {
                    var token = Generate(user, userLogin.roleId, userLogin.cajaId);
                    var tokenUsu = new TokenRole();
                    tokenUsu.token = token;
                    tokenUsu.permiso = permisoService.GetPermisoByRol(userLogin.roleId);
                    return Ok(tokenUsu);
                }

                return NotFound("Solicitud Denegada");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        private string GenerateInicial(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, user.UsuPNombre),
                new Claim(ClaimTypes.Email, user.UsuEmail)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(2),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string Generate(Usuario user, int roleId, int cajaId)
        {
            var checkRole = user.Rols.Where(x => x.RolId == roleId);
            if (checkRole.IsNullOrEmpty())
            {
                throw new BusinessException(Constants.ROLENOTFOUND);
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UsuPNombre),
                new Claim(ClaimTypes.Email, user.UsuEmail),
                new Claim(ClaimTypes.GivenName, user.UsuPNombre),
                new Claim(ClaimTypes.Role, roleId.ToString()),
                new Claim(ClaimTypes.Surname, user.UsuPApellido),
                new Claim(ClaimTypes.SerialNumber, user.UsuId.ToString()),
                new Claim(ClaimTypes.GroupSid, cajaId.ToString())
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
