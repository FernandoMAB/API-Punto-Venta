using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public partial class Permiso
    {
        public int PerId { get; set; }
        public int? RolId { get; set; }
        public string? PerPantalla { get; set; }
        public string? PerEstado { get; set; }
        [JsonIgnore]
        public virtual Rol? Rol { get; set; }
    }
}
