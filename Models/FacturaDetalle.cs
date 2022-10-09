using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public partial class FacturaDetalle
    {
        public FacturaDetalle()
        {
            Pros = new HashSet<Producto>();
        }

        public int FadId { get; set; }
        public int FacId { get; set; }
        public DateTime? FadFecha { get; set; }
        public double? FadPrecioUnit { get; set; } = 0;
        public int? FadCantidad { get; set; } = 0;
        public double? FadSubtotal { get; set; } = 0;
        public double? FadIva { get; set; } = 0;
        public double? FadDescuento { get; set; } = 0;
        public double? FadTotal { get; set; } = 0;
        public string? FadNumeracion { get; set; }
        public string? FadEstado { get; set; }

        [JsonIgnore]
        public virtual Factura? Fac { get; set; }

        public virtual ICollection<Producto>? Pros { get; set; }
    }
}
