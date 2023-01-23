using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public partial class Caja
    {
        public Caja()
        {
            Usus = new HashSet<Usuario>();
        }

        /// <summary>
        /// Código de la Caja.
        /// </summary>
        /// <example>12</example>
        public int CajId { get; set; }

        /// <summary>
        /// Número de monedas de 1 centavo.
        /// </summary>
        /// <example>10</example>
        public int? CajMon1C { get; set; }

        /// <summary>
        /// Número de monedas de 5 centavos.
        /// </summary>
        /// <example>10</example>
        public int? CajMon5C { get; set; }

        /// <summary>
        /// Número de monedas de 10 centavos.
        /// </summary>
        /// <example>5</example>
        public int? CajMon10C { get; set; }

        /// <summary>
        /// Número de monedas de 25 centavos.
        /// </summary>
        /// <example>4</example>
        public int? CajMon25C { get; set; }

        /// <summary>
        /// Número de monedas de 50 centavos.
        /// </summary>
        /// <example>6</example>
        public int? CajMon50C { get; set; }

        /// <summary>
        /// Número de monedas de 1 dolar.
        /// </summary>
        /// <example>10</example>
        public int? CajMon1Dol { get; set; }

        /// <summary>
        /// Número de billetes de 1 dolar.
        /// </summary>
        /// <example>5</example>
        public int? CajBill1Dol { get; set; }

        /// <summary>
        /// Número de billetes de 2 dolares.
        /// </summary>
        /// <example>0</example>
        public int? CajBill2Dol { get; set; }

        /// <summary>
        /// Número de billetes de 5 dolares.
        /// </summary>
        /// <example>10</example>
        public int? CajBill5Dol { get; set; }

        /// <summary>
        /// Número de billetes de 10 dolares.
        /// </summary>
        /// <example>20</example>
        public int? CajBill10Dol { get; set; }

        /// <summary>
        /// Número de billetes de 20 dolares.
        /// </summary>
        /// <example>5</example>
        public int? CajBill20Dol { get; set; }

        /// <summary>
        /// Número de billetes de 50 dolares.
        /// </summary>
        /// <example>0</example>
        public int? CajBill50Dol { get; set; }

        /// <summary>
        /// Número de billetes de 100 dolares.
        /// </summary>
        /// <example>1</example>
        public int? CajBill100Dol { get; set; }

        /// <summary>
        /// Caja Registro al ingreso del turno.
        /// </summary>
        /// <example>225.10</example>
        public double? CajRegIngreso { get; set; }

        /// <summary>
        /// Caja Registro al salir del turno.
        /// </summary>
        /// <example>270.10</example>
        public double? CajRegSalida { get; set; }

        /// <summary>
        /// Caja total de dinero en el momento del registro.
        /// </summary>
        /// <example>225.10</example>
        public double? CajTotal { get; set; }

        /// <summary>
        /// Fecha de ingreso caja.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime? CajFecha { get; set; }

        /// <summary>
        /// Estado de la caja, V: Vigente  -  E: Eliminado..
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string? CajEstado { get; set; }

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
        public virtual ICollection<Usuario> Usus { get; set; }
    }
}
