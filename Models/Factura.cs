using System;
using System.Collections.Generic;

namespace API_Punto_Venta.Models
{
    public partial class Factura
    {
        public Factura()
        {
            FacturaDetalles = new HashSet<FacturaDetalle>();
            Kardices = new HashSet<Kardex>();
        }

        public int FacId { get; set; }
        public int? CliId { get; set; }
        public int UsuId { get; set; }
        public DateTime? FacFecha { get; set; }
        public double? FacSubtotal { get; set; }
        public double? FacIva { get; set; }
        public double? FacDescuen { get; set; }
        public double? FacTotal { get; set; }
        public string? FacEstado { get; set; }

        public virtual Cliente? Cli { get; set; }
        public virtual Usuario Usu { get; set; } = null!;
        public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; }
        public virtual ICollection<Kardex> Kardices { get; set; }
    }
}
