using System;
using System.Collections.Generic;

namespace API_Punto_Venta.Models
{
    public partial class Kardex
    {
        public int KarId { get; set; }
        public int? FacId { get; set; }
        public int? ProId { get; set; }
        public int? ComId { get; set; }
        public DateTime? KarFecha { get; set; }
        public string? KarDetalle { get; set; }
        public int? KarEntCantidad { get; set; }
        public int? KarSalCantidad { get; set; }
        public double? KarEntPreTotal { get; set; }
        public double? KarSalPreTotal { get; set; }
        public int? KarBalCantidad { get; set; }
        public double? KarBalPrecio { get; set; }
        public double? KarBalPrecioTotal { get; set; }
        public string? KarEstado { get; set; }

        public virtual Compra? Com { get; set; }
        public virtual Factura? Fac { get; set; }
        public virtual Producto? Pro { get; set; }
    }
}
