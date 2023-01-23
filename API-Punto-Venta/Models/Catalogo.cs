using System.ComponentModel.DataAnnotations;

namespace API_Punto_Venta.Models
{
    public class Catalogo
    {
        /// <summary>
        /// Código del Catálogo.
        /// </summary>
        /// <example>12</example>
        public int CataId { get; set; }
        
        /// <summary>
        /// Nombre del Catálogo.
        /// </summary>
        /// <example>cl_tipo_iden</example>
        [MaxLength(100)]
        public string CataNombre { get; set; }
        
        /// <summary>
        /// Código del Catálogo.
        /// </summary>
        /// <example>CED</example>
        [MaxLength(10)]
        public string CataCodigo { get; set; }
        
        /// <summary>
        /// Valor del Catálogo.
        /// </summary>
        /// <example>CEDULA</example>
        [MaxLength(100)]
        public string CataValor { get; set; }
        
        /// <summary>
        /// Estado del Catálogo, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string CataEstado { get; set; }
        
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

        public Catalogo(string cataNombre, string cataCodigo, string cataValor, string cataEstado, int cataId)
        {
            CataId = cataId;
            CataNombre = cataNombre;
            CataCodigo = cataCodigo;
            CataValor = cataValor;
            CataEstado = cataEstado;
        }
    }
}
