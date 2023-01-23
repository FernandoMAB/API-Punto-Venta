using System.ComponentModel.DataAnnotations;

namespace API_Punto_Venta.Models
{
    public class UserLogin
    {
        /// <summary>
        /// Nombre de Usuario.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        
        /// <summary>
        /// Contraseña de ingreso del usuario.
        /// </summary>
        /// <example>ContraseñaSegura</example>
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        
        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        /// <example>bruno@hotmail.com</example>
        [MaxLength(50)]
        public string EmailAddress { get; set; } = string.Empty;
        
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        /// <example>Bruno</example>
        [MaxLength(50)]
        public string GivenName { get; set; } = string.Empty;
        
        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        /// <example>Dueñas</example>
        [MaxLength(50)]
        public string Surname { get; set; } = string.Empty;
        
        /// <summary>
        /// Rol del usuario.
        /// </summary>
        /// <example>Cajero</example>
        public string Role { get; set; } = string.Empty;
        
        /// <summary>
        /// Código Id de rol del usuario.
        /// </summary>
        /// <example>1</example>
        public int RoleId { get; set; } = 0;
        
        /// <summary>
        /// Código Id de la caja del usuario.
        /// </summary>
        /// <example>2</example>
        public int CajaId { get; set; } = 0;
        
        /// <summary>
        /// Código Id del usuario.
        /// </summary>
        /// <example>2</example>
        public int Id { get; set; } = 0;
    }

    public class TokenUsu
    {
        /// <summary>
        /// Token bearer.
        /// </summary>
        /// <example>ey JhbGciOiJIUzI1Ni...</example>
        public string Token { get; set; } = string.Empty;

        public virtual ICollection<Rol> Roles { get; set; }
        public virtual ICollection<Caja> Cajas { get; set; }
    }
    public class TokenRole
    {
        /// <summary>
        /// Token bearer.
        /// </summary>
        /// <example>ey JhbGciOiJIUzI1Ni...</example>
        public string Token { get; set; } = string.Empty;
        public IEnumerable<Permiso> permiso { get; set; }
    }
}