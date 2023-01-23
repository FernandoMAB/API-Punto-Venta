
namespace API_Punto_Venta.Models
{
    public class Compra
    {
        public Compra()
        {
            Kardices = new HashSet<Kardex>();
            Productos = new HashSet<Producto>();
        }

        public int ComId { get; set; }
        public DateTime? ComFecIngreso { get; set; }
        public double? ComIva { get; set; }
        public double? ComSubtotal { get; set; }
        public double? ComTotal { get; set; }
        
        public virtual ICollection<Kardex> Kardices { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
