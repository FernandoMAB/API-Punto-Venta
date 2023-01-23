namespace API_Punto_Venta.Models
{
    public class DashBoard
    {
        public DashBoard() { }
        public DashBoard(string propiedad, double valor)
        {
            Propiedad = propiedad;
            Valor = valor;
        }

        /// <summary>
        /// Propiedad principal del Dashboard.
        /// </summary>
        /// <example>Manicho</example>
        public string? Propiedad { get; set; }
        
        /// <summary>
        /// Valor principal del Dashboard.
        /// </summary>
        /// <example>129</example>
        public double? Valor { get; set; }

        /// <summary>
        /// Double 1 genérico del Dashboard.
        /// </summary>
        /// <example>232.2</example>
        public double? Double1 { get; set; }
        
        /// <summary>
        /// Double 2 genérico del Dashboard.
        /// </summary>
        /// <example>232.2</example>
        public double? Double2 { get; set; }
        
        /// <summary>
        /// Double 3 genérico del Dashboard.
        /// </summary>
        /// <example>232.2</example>
        public double? Double3 { get; set; }
        
        /// <summary>
        /// Integer 1 genérico del Dashboard.
        /// </summary>
        /// <example>10</example>
        public int? Int1 { get; set; }
        
        /// <summary>
        /// Integer 2 genérico del Dashboard.
        /// </summary>
        /// <example>10</example>
        public int? Int2 { get; set; }
        
        /// <summary>
        /// Integer 3 genérico del Dashboard.
        /// </summary>
        /// <example>10</example>
        public int? Int3 { get; set; }
        
        /// <summary>
        /// String 1 genérico del Dashboard.
        /// </summary>
        /// <example>string1</example>
        public string? String1 { get; set; }
        
        /// <summary>
        /// String 2 genérico del Dashboard.
        /// </summary>
        /// <example>string2</example>
        public string? String2 { get; set; }
        
        /// <summary>
        /// String 3 genérico del Dashboard.
        /// </summary>
        /// <example>string3</example>
        public string? String3 { get; set; }
        
        /// <summary>
        /// DateTime 1 genérico del Dashboard.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime DateTime1 { get; set; }
        
        /// <summary>
        /// DateTime 2 genérico del Dashboard.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime DateTime2 { get; set; }
        
        /// <summary>
        /// DateTime 3 genérico del Dashboard.
        /// </summary>
        /// <example>2022-12-26T23:35:58.733Z</example>
        public DateTime DateTime3 { get; set; }
    }
}
