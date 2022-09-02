using System;
using System.Collections.Generic;

namespace API_Punto_Venta.Models
{
    public partial class Caja
    {
        public Caja()
        {
            Usus = new HashSet<Usuario>();
        }

        public int CajId { get; set; }
        public int? CajMon1C { get; set; }
        public int? CajMon5C { get; set; }
        public int? CajMon10C { get; set; }
        public int? CajMon25C { get; set; }
        public int? CajMon50C { get; set; }
        public int? CajMon1Dol { get; set; }
        public int? CajBill1Dol { get; set; }
        public int? CajBill2Dol { get; set; }
        public int? CajBill5Dol { get; set; }
        public int? CajBill10Dol { get; set; }
        public int? CajBill20Dol { get; set; }
        public int? CajBill50Dol { get; set; }
        public int? CajBill100Dol { get; set; }
        public double? CajRegIngreso { get; set; }
        public double? CajRegSalida { get; set; }
        public double? CajTotal { get; set; }
        public DateTime? CajFecha { get; set; }
        public string? CajEstado { get; set; }

        public virtual ICollection<Usuario> Usus { get; set; }
    }
}
