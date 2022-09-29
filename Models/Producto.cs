using System;
using System.Collections.Generic;

namespace API_Punto_Venta.Models
{
    public partial class Producto
    {
        public Producto()
        {
            CategoriaProductos = new HashSet<CategoriaProducto>();
            Kardices = new HashSet<Kardex>();
            Fads = new HashSet<FacturaDetalle>();
        }

        public int ProId { get; set; }
        public int? ComId { get; set; }
        public string? ProNombre { get; set; }
        public float? ProDescuento { get; set; }
        public double? ProPrecio { get; set; }
        public string? ProCodBarras { get; set; }
        public string? ProCategoria { get; set; }
        public string? ProMarca { get; set; }
        public int? ProEstIva { get; set; }
        public string? ProDetalle { get; set; }
        public string? ProEstado { get; set; }
        public int? ProStock { get; set; }
        public int? ProCantidad { get; set; }

        public virtual Compra? Com { get; set; }
        public virtual ICollection<CategoriaProducto> CategoriaProductos { get; set; }
        public virtual ICollection<Kardex> Kardices { get; set; }

        public virtual ICollection<FacturaDetalle> Fads { get; set; }
    }
}
