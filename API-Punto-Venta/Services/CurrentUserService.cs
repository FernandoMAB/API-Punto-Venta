using System.Security.Claims;

namespace API_Punto_Venta.Services
{
    public class CurrentUserService : ICurrentUserService
    {

        public CurrentUserService ()
        { 
            
        }

        public int getCurrentUserId(ClaimsPrincipal _user)
        {
            ClaimsIdentity? identity = _user.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                var idC = 0;
                if (userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value != null)
                {
                    idC = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.SerialNumber)?.Value);
                }
                return idC;
            }
            return 0;
        }
    }
    public interface ICurrentUserService
    {
        int getCurrentUserId(ClaimsPrincipal user);
    }
}
