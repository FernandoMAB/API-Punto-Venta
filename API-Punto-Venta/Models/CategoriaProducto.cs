using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class CategoriaProducto
    {
        public int CaProId { get; set; }
        public int? ProId { get; set; }
        public int? CatId { get; set; }
        public string? CaProEstado { get; set; }

        public virtual Categorium? Cat { get; set; }
        [JsonIgnore]
        public virtual Producto? Pro { get; set; }
    }
}
