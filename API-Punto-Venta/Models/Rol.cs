using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class Rol
    {
        public Rol()
        {
            Permisos = new HashSet<Permiso>();
            Usus = new HashSet<Usuario>();
        }

        /// <summary>
        /// Código Id del rol.
        /// </summary>
        /// <example>1</example>
        public int RolId { get; set; }
        
        /// <summary>
        /// Descripción del Rol.
        /// </summary>
        /// <example>Rol de Prueba</example>
        [MaxLength(500)]
        public string? RolDescrip { get; set; }
        
        /// <summary>
        /// Estado del rol, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? RolEstado { get; set; }
        
        /// <summary>
        /// Usuario de creación del rol.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioIngreso { get; set; }
        
        /// <summary>
        /// Usuario de modificación del rol.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioModificacion { get; set; }
        
        /// <summary>
        /// Fecha de creción del rol.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaIngreso { get; set; }
        
        /// <summary>
        /// Fecha de modificación del rol.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaModificacion { get; set; }
        [JsonIgnore]
        public virtual ICollection<Permiso> Permisos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Usuario> Usus { get; set; }
    }
}
