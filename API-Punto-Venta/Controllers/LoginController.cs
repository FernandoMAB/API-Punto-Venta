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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_Punto_Venta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly PuntoVentaContext _context;
        private readonly IPermisoService _permisoService;


        public LoginController(IConfiguration config, PuntoVentaContext dbcontext, IPermisoService permisoService)
        {
            _config = config;
            _context = dbcontext;
            _permisoService = permisoService;
        }

        /// <summary>
        /// Realiza el inicio de sesión primera fase.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza el inicio de sesión de la primera fase.
        /// </remarks>
        /// <param name="userLogin"> Datos de Login.</param>
        /// 
        /// <response code="200">Se ha realizado la eliminación exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(TokenUsu),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody, BindRequired] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user == null) return Conflict(new MessageErrorConflict(Constants.ERRBUSEX, "Solicitud Denegada"));
            
            var token = GenerateInicial(user);
            var tokenUsu = new TokenUsu
            {
                Token = token,
                Roles = user.Rols,
                Cajas = user.Cajs
            };
            return Ok(tokenUsu);

        }

        /// <summary>
        /// Realiza el inicio de sesión selección de roles y cajas.
        /// </summary>
        ///
        /// <remarks>
        /// Realiza el inicio de sesión selección de roles y cajas segunda fase.
        /// </remarks>
        /// <param name="userLogin"> Datos de Login.</param>
        /// 
        /// <returns>Retorna el token con el que se va a poder navegar.</returns>
        /// <response code="200">Se ha realizado la consulta exitosamente.</response>
        /// <response code="400">El mensaje de solicitud no se encuentra debidamente formateado.</response>
        /// <response code="401">No se encuentra autorizado para ejecutar la operación.</response>
        /// <response code="404">El objeto no fue encontrado.</response>
        /// <response code="409">La operación presentó un error durante la ejecución.</response>
        /// <response code="500">Se presentó un error durante el procesamiento de la solicitud.</response>
        [Authorize]
        [HttpPost("SelectRole")]
        [ProducesResponseType(typeof(TokenRole),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageErrorNotFound),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageErrorConflict),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageErrorProblem),StatusCodes.Status500InternalServerError)]
        public IActionResult SelectRole([FromBody, BindRequired] UserLogin userLogin)
        {
            try
            {
                if (userLogin.RoleId == 0)
                {
                    return BadRequest(new MessageErrorConflict(Constants.ERRBUSEX,"Solicitar a Administración el ingreso de un Rol"));
                }
                var user = Authenticate(userLogin);

                if (user == null) return Conflict(new MessageErrorConflict(Constants.ERRBUSEX, "Solicitud Denegada"));
                var token = Generate(user, userLogin.RoleId, userLogin.CajaId);
                var tokenUsu = new TokenRole
                {
                    Token = token,
                    permiso = _permisoService.GetPermisoByRol(userLogin.RoleId)
                };
                return Ok(tokenUsu);

            }
            catch (NotFoundException ex)
            {
                return NotFound(new MessageErrorNotFound(Constants.ERRNOTFU,ex.Message));
            }
            catch (BusinessException ex)
            {
                return Conflict(new MessageErrorConflict(Constants.ERRBUSEX,ex.Message));
            }
            catch (Exception ex)
            {
                return Problem(new MessageErrorProblem(Constants.ERRPROBM,ex.Message).ToString());
            }
        }

        private string GenerateInicial(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            
            if (user.UsuUserName != null) claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UsuUserName));
            if (user.UsuEmail != null) claims.Add(new Claim(ClaimTypes.Email, user.UsuEmail));


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

            if (cajaId != 0)
            {
                var checkCaja = user.Cajs.Where(x => x.CajId == cajaId);
                if (checkCaja.IsNullOrEmpty())
                {
                    throw new BusinessException(Constants.CAJANOTFOUND);
                }
            }
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            if (user.UsuUserName != null) claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UsuUserName));
            if (user.UsuEmail != null) claims.Add(new Claim(ClaimTypes.Email, user.UsuEmail));
            if (user.UsuPNombre != null) claims.Add(new Claim(ClaimTypes.GivenName, user.UsuPNombre));
            claims.Add(new Claim(ClaimTypes.Role, roleId.ToString()));
            if (user.UsuPApellido != null) claims.Add(new Claim(ClaimTypes.Surname, user.UsuPApellido));
            claims.Add(new Claim(ClaimTypes.SerialNumber, user.UsuId.ToString()));
            claims.Add(new Claim(ClaimTypes.GroupSid, cajaId.ToString()));
            
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
                var currentUser = _context.Usuarios.Include(x => x.Rols)
                                                    .Include(x => x.Cajs)
                                                    .FirstOrDefault(o => o.UsuUserName.ToLower() ==
                userLogin.UserName.ToLower() && o.UsuContrasena == userLogin.Password);

                if (currentUser !=null)
                {
                    return currentUser;
                }

                throw new NotFoundException("Usuario no encontrado");
            }catch(Exception ex){
                return null!;
            }
        }
    }
}
