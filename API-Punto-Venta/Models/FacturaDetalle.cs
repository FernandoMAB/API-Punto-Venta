using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class FacturaDetalle
    {
        public FacturaDetalle()
        {
            Pros = new HashSet<Producto>();
        }

        /// <summary>
        /// Código de la Factura.
        /// </summary>
        /// <example>12</example>
        public int FadId { get; set; }
        
        /// <summary>
        /// Código de la Factura Detalle.
        /// </summary>
        /// <example>16</example>
        public int FacId { get; set; }
        
        /// <summary>
        /// Fecha actual de la factura detalle.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FadFecha { get; set; }
        
        /// <summary>
        /// Precio Unitario de la factura detalle.
        /// </summary>
        /// <example>1.50</example>
        public double? FadPrecioUnit { get; set; } = 0;
        
        /// <summary>
        /// Cantidad de productos de la factura detalle.
        /// </summary>
        /// <example>4</example>
        public int? FadCantidad { get; set; } = 0;
        
        /// <summary>
        /// Subtotal de la factura detalle.
        /// </summary>
        /// <example>6</example>
        public double? FadSubtotal { get; set; } = 0;
        
        /// <summary>
        /// Valor del IVA de la factura detalle.
        /// </summary>
        /// <example>0.72</example>
        public double? FadIva { get; set; } = 0;
        
        /// <summary>
        /// Valor de los descuentos de la factura detalle.
        /// </summary>
        /// <example>0</example>
        public double? FadDescuento { get; set; } = 0;
        
        /// <summary>
        /// Valor total de la factura detalle.
        /// </summary>
        /// <example>6.72</example>
        public double? FadTotal { get; set; } = 0;
        
        /// <summary>
        /// Numeración de la factura detalle.
        /// </summary>
        /// <example>0210202201130832096700110010010000000010326598618</example>
        [MaxLength(100)]
        public string? FadNumeracion { get; set; }
        
        /// <summary>
        /// Estado de la Factura Detalle, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? FadEstado { get; set; }
        
        /// <summary>
        /// Código Id del producto asociado a la factura detalle.
        /// </summary>
        /// <example>12</example>
        public int? ProId { get; set; }

        [JsonIgnore]
        public virtual Factura? Fac { get; set; }
        
        public virtual ICollection<Producto>? Pros { get; set; }
    }
}
