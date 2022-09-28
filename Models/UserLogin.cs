namespace API_Punto_Venta.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public int roleId { get; set; } = 0;
        public int cajaId { get; set; } = 0;
    }

    public class TokenUsu
    {
        public string token {get;set;}

        public virtual ICollection<Rol> Roles { get; set; }
        public virtual ICollection<Caja> Cajas { get; set; }
    }
    public class TokenRole
    {
        public string token { get; set; }
        public IEnumerable<Permiso> permiso { get; set; }
    }
}