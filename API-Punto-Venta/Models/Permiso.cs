using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class Permiso
    {
        /// <summary>
        /// Código Id del permiso.
        /// </summary>
        /// <example>2</example>
        public int PerId { get; set; }
        
        /// <summary>
        /// Código Id del rol.
        /// </summary>
        /// <example>1</example>
        public int? RolId { get; set; }
        
        /// <summary>
        /// Pantallas asociadas al permiso.
        /// </summary>
        /// <example>DASHBOARD, VENTAS, CLIENTES</example>
        [MaxLength(200)]
        public string? PerPantalla { get; set; }
        
        /// <summary>
        /// Estado del permiso, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? PerEstado { get; set; }
        
        /// <summary>
        /// Usuario de creación del permiso.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioIngreso { get; set; }
        
        /// <summary>
        /// Usuario de modificación del permiso.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioModificacion { get; set; }
        
        /// <summary>
        /// Fecha de creción del permiso.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaIngreso { get; set; }
        
        /// <summary>
        /// Fecha de modificación del permiso.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaModificacion { get; set; }
        [JsonIgnore]
        public virtual Rol? Rol { get; set; }
    }
}
