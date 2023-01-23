using System.ComponentModel.DataAnnotations;

namespace API_Punto_Venta.Models
{
    public class Statistic
    {

        /// <summary>
        /// Año de la estadística.
        /// </summary>
        /// <example>2018</example>
        public int EstYear { get; set; } = 0;
        
        /// <summary>
        /// Mes de la estadística.
        /// </summary>
        /// <example>Enero</example>
        [MaxLength(100)]
        public string EstMonth { get; set; } = string.Empty;
        
        /// <summary>
        /// Semana de la estadística.
        /// </summary>
        /// <example>1</example>
        public int EstWeek { get; set; } = 0;
        
        /// <summary>
        /// Total de ventas de la estadística.
        /// </summary>
        /// <example>560.80</example>
        public decimal? EstTotalSold { get; set; } = default;


    }
}
