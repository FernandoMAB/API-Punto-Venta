namespace API_Punto_Venta.Models
{
    public class Catalogo
    {
        public string CataNombre { get; set; }
        public string CataCodigo { get; set; }
        public string CataValor { get; set; }
        public string CataEstado { get; set; }

        public Catalogo(string cataNombre, string cataCodigo, string cataValor, string cataEstado)
        {
            CataNombre = cataNombre;
            CataCodigo = cataCodigo;
            CataValor = cataValor;
            CataEstado = cataEstado;
        }
    }
}
