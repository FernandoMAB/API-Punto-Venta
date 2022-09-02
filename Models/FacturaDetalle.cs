using System;
using System.Collections.Generic;

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
        public double? FadPrecioUnit { get; set; }
        public int? FadCantidad { get; set; }
        public double? FadSubtotal { get; set; }
        public double? FadIva { get; set; }
        public double? FadDescuento { get; set; }
        public double? FadTotal { get; set; }
        public string? FadNumeracion { get; set; }
        public string? FadEstado { get; set; }

        public virtual Factura Fac { get; set; } = null!;

        public virtual ICollection<Producto> Pros { get; set; }
    }
}
