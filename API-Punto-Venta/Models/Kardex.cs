
namespace API_Punto_Venta.Models
{
    public class Kardex
    {

        /// <summary>
        /// Código del Kardex.
        /// </summary>
        /// <example>12</example>
        public int KarId { get; set; }

        /// <summary>
        /// Código de la Factura.
        /// </summary>
        /// <example>12</example>
        public int? FacId { get; set; }

        /// <summary>
        /// Código Id del producto.
        /// </summary>
        /// <example>12</example>
        public int? ProId { get; set; }

        /// <summary>
        /// Código Id de la compra.
        /// </summary>
        /// <example>12</example>
        public int? ComId { get; set; }

        /// <summary>
        /// Fecha de ingreso del Kardex.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? KarFecha { get; set; }

        /// <summary>
        /// Detalle del registro Kardex.
        /// </summary>
        /// <example>Ingreso de nueva mercaderia</example>
        public string? KarDetalle { get; set; }

        /// <summary>
        /// Cantidad de unidades ingresadas al Kardex.
        /// </summary>
        /// <example>100</example>
        public int? KarEntCantidad { get; set; }

        /// <summary>
        /// Cantidad de unidades de salida del Kardex.
        /// </summary>
        /// <example>10</example>
        public int? KarSalCantidad { get; set; }

        /// <summary>
        /// Precio de entrada por unidad al Kardex.
        /// </summary>
        /// <example>0.80</example>
        public double? KarEntPrecio { get; set; }

        /// <summary>
        /// Precio de salida por unidad del Kardex.
        /// </summary>
        /// <example>1</example>
        public double? KarSalPrecio { get; set; }

        /// <summary>
        /// Precio de entrada total al Kardex.
        /// </summary>
        /// <example>8</example>
        public double? KarEntPreTotal { get; set; }

        /// <summary>
        /// Precio de salida total del Kardex.
        /// </summary>
        /// <example>10</example>
        public double? KarSalPreTotal { get; set; }

        /// <summary>
        /// Balance de unidades del Kardex.
        /// </summary>
        /// <example>120</example>
        public int? KarBalCantidad { get; set; }

        /// <summary>
        /// Balance de precio del producto en el Kardex.
        /// </summary>
        /// <example>0.82</example>
        public double? KarBalPrecio { get; set; }

        /// <summary>
        /// Balance de precio total del producto en el Kardex.
        /// </summary>
        /// <example>98.4</example>
        public double? KarBalPrecioTotal { get; set; }

        /// <summary>
        /// Estado del kardex, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        public string? KarEstado { get; set; }


        /// <summary>
        /// Compras relacionadas con el kardex
        /// </summary>
        public virtual Compra? Com { get; set; }

        /// <summary>
        /// Facturas relacionadas con el kardex
        /// </summary>
        public virtual Factura? Fac { get; set; }

        /// <summary>
        /// Productos relacionadas con el kardex
        /// </summary>
        public virtual Producto? Pro { get; set; }
    }
}
