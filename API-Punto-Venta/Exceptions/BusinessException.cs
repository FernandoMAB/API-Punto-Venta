namespace API_Punto_Venta.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        { }
        public BusinessException(string message) : base(message) { }
    }
}
