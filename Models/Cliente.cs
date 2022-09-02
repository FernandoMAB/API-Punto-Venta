using System;
using System.Collections.Generic;

namespace API_Punto_Venta.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int CliId { get; set; }
        public string? CliPNombre { get; set; }
        public string? CliPApellido { get; set; }
        public string? CliSNombre { get; set; }
        public string? CliSApellido { get; set; }
        public string? CliTipoIden { get; set; }
        public string? CliNumeroIden { get; set; }
        public string? CliDireccion { get; set; }
        public string? CliEmail { get; set; }
        public string? CliNumCelular { get; set; }
        public string? CliTelefono { get; set; }
        public string? CliEstado { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
