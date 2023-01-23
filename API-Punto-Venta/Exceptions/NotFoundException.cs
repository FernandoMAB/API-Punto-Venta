namespace API_Punto_Venta.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        { }
        public NotFoundException(string message) : base(message) { }
    }
}
