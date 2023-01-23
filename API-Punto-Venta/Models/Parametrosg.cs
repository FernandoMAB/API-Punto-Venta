
using System.ComponentModel.DataAnnotations;

namespace API_Punto_Venta.Models
{
    public class Parametrosg
    {
        /// <summary>
        /// Código del parámetro.
        /// </summary>
        /// <example>22</example>
        public int ParId { get; set; }
        
        /// <summary>
        /// Descripción del parámetro.
        /// </summary>
        /// <example>Codigo Secuencial para la factura</example>
        [MaxLength(500)]
        public string? ParDescrip { get; set; }
        
        /// <summary>
        /// Valor del parámetro.
        /// </summary>
        /// <example>00000074</example>
        [MaxLength(300)]
        public string? ParValor { get; set; }
        
        /// <summary>
        /// Estado del parámetro, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? ParEstado { get; set; }
        
        /// <summary>
        /// Nombre del nemónico del parámetro.
        /// </summary>
        /// <example>SECU</example>
        [MaxLength(5)]
        public string? ParNemonico { get; set;}
        
        /// <summary>
        /// Usuario de creación del parámetro.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioIngreso { get; set; }
        
        /// <summary>
        /// Usuario de modificación del parámetro.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioModificacion { get; set; }
        
        /// <summary>
        /// Fecha de creción del parámetro.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaIngreso { get; set; }
        
        /// <summary>
        /// Fecha de modificación del parámetro.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaModificacion { get; set; }
    }
}
