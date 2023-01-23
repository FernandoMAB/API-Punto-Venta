
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Kardices = new HashSet<KardexDto>();
        }

        public int? ProId { get; set; }
        [JsonIgnore]
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
        public int? ProStock { get; set; } = 0;
        public int? ProCantidad { get; set; } = 0;

        public virtual ICollection<KardexDto> Kardices { get; set; }
    }
}
