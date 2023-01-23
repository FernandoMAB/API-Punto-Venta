using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Facturas = new HashSet<Factura>();
            Cajs = new HashSet<Caja>();
            Rols = new HashSet<Rol>();
            Documentos = new HashSet<Documento>();
        }

        /// <summary>
        /// Código Id del usuario.
        /// </summary>
        /// <example>2</example>
        public int UsuId { get; set; }
        
        /// <summary>
        /// Primer nombre del usuario.
        /// </summary>
        /// <example>John</example>
        [MaxLength(50)]
        public string? UsuPNombre { get; set; }
        
        /// <summary>
        /// Primer apellido del usuario.
        /// </summary>
        /// <example>Doe</example>
        [MaxLength(50)]
        public string? UsuPApellido { get; set; }
        
        /// <summary>
        /// Segundo nombre del usuario.
        /// </summary>
        /// <example>Ricardo</example>
        [MaxLength(50)]
        public string? UsuSNombre { get; set; }
        
        /// <summary>
        /// Segundo apellido del usuario.
        /// </summary>
        /// <example>Perez</example>
        [MaxLength(50)]
        public string? UsuSApellido { get; set; }
        
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        /// <example>Cotnrseña segura</example>
        [MaxLength(50)]
        public string? UsuContrasena { get; set; }
        
        /// <summary>
        /// Tipo de identificación del usuario.
        /// </summary>
        /// <example>CED</example>
        [MaxLength(4)]
        public string? UsuTipoIden { get; set; }
        
        /// <summary>
        /// Número de identificación del usuario.
        /// </summary>
        /// <example>1734894305</example>
        [MaxLength(30)]
        public string? UsuNumeroIden { get; set; }
        
        /// <summary>
        /// Fecha de nacimiento del usaurio.
        /// </summary>
        /// <example>2000-12-26T23:35:58.733Z</example>
        public DateTime? UsuFecNacimiento { get; set; }
        
        /// <summary>
        /// Estado civil del usuario.
        /// </summary>
        /// <example>SOLT</example>
        [MaxLength(4)]
        public string? UsuEstCivil { get; set; }
        
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        /// <example>john.doe@mail.com</example>
        [MaxLength(50)]
        [EmailAddress]
        public string? UsuEmail { get; set; }
        
        /// <summary>
        /// Teléfono del usuario.
        /// </summary>
        /// <example>2456386</example>
        [MaxLength(30)]
        public string? UsuTelefono { get; set; }
        
        /// <summary>
        /// Número celular del usuario.
        /// </summary>
        /// <example>+593 984533023</example>
        [MaxLength(30)]
        public string? UsuNumCelular { get; set; }
        
        /// <summary>
        /// Dirección de domicilio del usuario.
        /// </summary>
        /// <example>Av Orellana y 6 de diciembre, Quito-Ecuador</example>
        [MaxLength(500)]
        public string? UsuDireccion { get; set; } = string.Empty;
        
        /// <summary>
        /// Número de cargas familiares del usuario.
        /// </summary>
        /// <example>1</example>
        public int? UsuNumCargas { get; set; } = 0;
        
        /// <summary>
        /// Estado del usuario, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? UsuEstado { get; set; }
        
        /// <summary>
        /// Nombre de usuario del cliente.
        /// </summary>
        /// <example>john.doe</example>
        [MaxLength(50)]
        public string? UsuUserName { get; set; }
        
        /// <summary>
        /// Usuario de creación del cliente.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioIngreso { get; set; }
        
        /// <summary>
        /// Usuario de modificación del cliente.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioModificacion { get; set; }
        
        /// <summary>
        /// Fecha de creción del cliente.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaIngreso { get; set; }
        
        /// <summary>
        /// Fecha de modificación del cliente.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaModificacion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Caja> Cajs { get; set; }
        public virtual ICollection<Rol> Rols { get; set; }
        [JsonIgnore]
        public virtual ICollection<Documento> Documentos { get; set; }
    }
}
