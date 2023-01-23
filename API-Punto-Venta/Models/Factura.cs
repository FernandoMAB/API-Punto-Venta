using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class Factura
    {
        public Factura()
        {
            FacturaDetalles = new HashSet<FacturaDetalle>();
            Kardices = new HashSet<Kardex>();
        }

        /// <summary>
        /// Código de la Factura.
        /// </summary>
        /// <example>12</example>
        public int FacId { get; set; }
        
        /// <summary>
        /// Código del cliente.
        /// </summary>
        /// <example>1</example>
        public int? CliId { get; set; }
        
        /// <summary>
        /// Código del usuario que atiende al cliente.
        /// </summary>
        /// <example>12</example>
        public int UsuId { get; set; }
        
        /// <summary>
        /// Fecha actual de la factura.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FacFecha { get; set; }
        
        /// <summary>
        /// Subtotal de la factura.
        /// </summary>
        /// <example>20.50</example>
        public double? FacSubtotal { get; set; }
        
        /// <summary>
        /// Total de IVA de la factura.
        /// </summary>
        /// <example>2.46</example>
        public double? FacIva { get; set; }
        
        /// <summary>
        /// Total de Descuentos de la factura.
        /// </summary>
        /// <example>0</example>
        public double? FacDescuen { get; set; }
        
        /// <summary>
        /// Total de la factura.
        /// </summary>
        /// <example>22.96</example>
        public double? FacTotal { get; set; }
        
        /// <summary>
        /// Estado de la Factura, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? FacEstado { get; set; }
        [JsonIgnore]
        
        public virtual Cliente? Cli { get; set; }
        [JsonIgnore]
        public virtual Usuario? Usu { get; set; } = null!;
        
        /// <summary>
        /// Facturas Detalles asociadas a la Factura.
        /// </summary>
        public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; }
        [JsonIgnore]
        public virtual ICollection<Kardex> Kardices { get; set; }
    }
}
