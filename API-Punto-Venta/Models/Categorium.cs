using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            CategoriaProductos = new HashSet<CategoriaProducto>();
        }

        public int CatId { get; set; }
        public string? CatDescrip { get; set; }
        public string? CatEstado { get; set; }

        [JsonIgnore]
        public virtual ICollection<CategoriaProducto> CategoriaProductos { get; set; }
    }
}
