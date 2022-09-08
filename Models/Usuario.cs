using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Facturas = new HashSet<Factura>();
            Cajs = new HashSet<Caja>();
            Rols = new HashSet<Rol>();
        }

        public int UsuId { get; set; }
        public string? UsuPNombre { get; set; }
        public string? UsuPApellido { get; set; }
        public string? UsuSNombre { get; set; }
        public string? UsuSApellido { get; set; }
        public string? UsuContrasena { get; set; }
        public string? UsuTipoIden { get; set; }
        public string? UsuNumeroIden { get; set; }
        public DateTime? UsuFecNacimiento { get; set; }
        public string? UsuEstCivil { get; set; }
        public string? UsuEmail { get; set; }
        public string? UsuTelefono { get; set; }
        public string? UsuNumCelular { get; set; }
        public string? UsuDireccion { get; set; }
        public int? UsuNumCargas { get; set; }
        public string? UsuEstado { get; set; }
        public string? UsuUserName { get; set; }
        public virtual Rol? Rol {get;set;}

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Caja> Cajs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Rol> Rols { get; set; }
    }
}
