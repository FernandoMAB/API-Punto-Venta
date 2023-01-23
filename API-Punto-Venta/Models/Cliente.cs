using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
            Documentos = new HashSet<Documento>();
        }
        
        /// <summary>
        /// Código del cliente.
        /// </summary>
        /// <example>12</example>
        public int CliId { get; set; }
        
        /// <summary>
        /// Primer nombre del cliente.
        /// </summary>
        /// <example>Mateo</example>
        [MaxLength(50)]
        public string? CliPNombre { get; set; }
        
        /// <summary>
        /// Primer apellido del cliente.
        /// </summary>
        /// <example>Cordova</example>
        [MaxLength(50)]
        public string? CliPApellido { get; set; }
        
        /// <summary>
        /// Segundo nombre del cliente.
        /// </summary>
        /// <example>Sebastian</example>
        [MaxLength(50)]
        public string? CliSNombre { get; set; }
        
        /// <summary>
        /// Segundo apellido del cliente.
        /// </summary>
        /// <example>Hurtado</example>
        [MaxLength(50)]
        public string? CliSApellido { get; set; }
        
        /// <summary>
        /// Tipo de identificación del cliente.
        /// </summary>
        /// <example>CED</example>
        [MaxLength(4)]
        public string? CliTipoIden { get; set; }
        
        /// <summary>
        /// Número de identificación del cliente.
        /// </summary>
        /// <example>1738591875</example>
        [MaxLength(30)]
        public string? CliNumeroIden { get; set; }
        
        /// <summary>
        /// Dirección del cliente.
        /// </summary>
        /// <example>Club los chillos</example>
        [MaxLength(500)]
        public string? CliDireccion { get; set; }
        
        /// <summary>
        /// Mail del cliente.
        /// </summary>
        /// <example>mateo.cordova.hurtado@udla.edu.ec</example>
        [MaxLength(50)]
        [EmailAddress]
        public string? CliEmail { get; set; }
        
        /// <summary>
        /// Número celular del cliente.
        /// </summary>
        /// <example>0984373445</example>
        [MaxLength(30)]
        public string? CliNumCelular { get; set; }
        
        /// <summary>
        /// Número de teléfono del cliente.
        /// </summary>
        /// <example>026749583</example>
        [MaxLength(30)]
        public string? CliTelefono { get; set; }
        
        /// <summary>
        /// Estado del cliente, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? CliEstado { get; set; }
        
        /// <summary>
        /// Usuario de creación del cliente.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioIngreso { get; set; }
        
        /// <summary>
        /// Usuario de modificación del cliente.
        /// </summary>
        /// <example>bruno123</example>
        [MaxLength(100)]
        public string? UsuarioModificacion { get; set; }
        
        /// <summary>
        /// Fecha de creción del cliente.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaIngreso { get; set; }
        
        /// <summary>
        /// Fecha de modificación del cliente.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? FechaModificacion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Factura> Facturas { get; set; }
        [JsonIgnore]
        public virtual ICollection<Documento> Documentos { get; set; }

    }
}
