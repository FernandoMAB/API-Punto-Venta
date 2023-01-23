using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class Producto
    {
        public Producto()
        {
            CategoriaProductos = new HashSet<CategoriaProducto>();
            Kardices = new HashSet<Kardex>();
            Fads = new HashSet<FacturaDetalle>();
        }

        /// <summary>
        /// Código Id del producto.
        /// </summary>
        /// <example>2</example>
        public int ProId { get; set; }
        [JsonIgnore]
        public int? ComId { get; set; }
        
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        /// <example>Manicho</example>
        [MaxLength(50)]
        public string? ProNombre { get; set; }
        
        /// <summary>
        /// Descuento del producto.
        /// </summary>
        /// <example>0</example>
        public float? ProDescuento { get; set; } 
        
        /// <summary>
        /// Precio del producto.
        /// </summary>
        /// <example>0.80</example>
        public double? ProPrecio { get; set; } = 0;
        
        /// <summary>
        /// Código de barras del producto.
        /// </summary>
        /// <example>A00012</example>
        [MaxLength(200)]
        public string? ProCodBarras { get; set; }
        
        /// <summary>
        /// Categoría del producto.
        /// </summary>
        /// <example>Chocolate con leche</example>
        [MaxLength(50)]
        public string? ProCategoria { get; set; }
        
        /// <summary>
        /// Marca del producto.
        /// </summary>
        /// <example>La universal</example>
        [MaxLength(50)]
        public string? ProMarca { get; set; }
        
        /// <summary>
        /// Estado IVA del producto.
        /// </summary>
        /// <example>12</example>
        public int? ProEstIva { get; set; }
        
        /// <summary>
        /// Detalle del producto.
        /// </summary>
        /// <example>Deliciosa barra de chocolate con leche e inclusiones de mani. Producto orgullosamente ecuatoriano. Contiene 0% trans.</example>
        [MaxLength(200)]
        public string? ProDetalle { get; set; }
        
        /// <summary>
        /// Estado del producto, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? ProEstado { get; set; }
        
        /// <summary>
        /// Cantidad de Stock del producto.
        /// </summary>
        /// <example>200</example>
        public int? ProStock { get; set; } = 0;
        
        /// <summary>
        /// Cantidad de productos que ingresan al negocio.
        /// </summary>
        /// <example>250</example>
        public int? ProCantidad { get; set; } = 0;
        
        /// <summary>
        /// Usuario de creación del producto.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioIngreso { get; set; }
        
        /// <summary>
        /// Usuario de modificación del producto.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioModificacion { get; set; }
        
        /// <summary>
        /// Fecha de creción del producto.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaIngreso { get; set; }
        
        /// <summary>
        /// Fecha de modificación del producto.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaModificacion { get; set; }

        [JsonIgnore]
        public virtual Compra? Com { get; set; }
        public virtual ICollection<CategoriaProducto> CategoriaProductos { get; set; }
        public virtual ICollection<Kardex> Kardices { get; set; }
        [JsonIgnore]
        public virtual ICollection<FacturaDetalle> Fads { get; set; }
    }
}
