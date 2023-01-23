namespace API_Punto_Venta.Exceptions;

public class MessageErrorNotFound
{
    /// <summary>
    /// Mensaje de Respuesta en caso de no encontrar el registro.
    /// </summary>
    /// <example>El objeto no fue encontrado.</example>
    public string? Message { get; set; }

    /// <summary>
    /// Detalle Respuesta en caso de no encontrar el registro.
    /// </summary>
    /// <example>No se encontró registro</example>
    public string? Detail { get; set; }

    public MessageErrorNotFound(string? message, String? detail)
    {
        Message = message;
        Detail = detail;
    }
}
    
public class MessageErrorConflict
{
    /// <summary>
    /// Mensaje de Respuesta en caso de error de negocio.
    /// </summary>
    /// <example>La operación presentó un error durante la ejecución.</example>
    public string? Message { get; set; }

    /// <summary>
    /// Detalle Respuesta en caso de error de negocio.
    /// </summary>
    /// <example>No se existe el stock necesario en el producto.</example>
    public string? Detail { get; set; }

    public MessageErrorConflict(string? message, string? detail)
    {
        Message = message;
        Detail = detail;
    }
}
    
public class MessageErrorProblem
{
    /// <summary>
    /// Mensaje de Respuesta en caso de error no controlado.
    /// </summary>
    /// <example>Se presentó un error durante el procesamiento de la solicitud.</example>
    public string? Message  { get; set; }

    /// <summary>
    /// Detalle Respuesta en caso de error no controlado.
    /// </summary>
    /// <example>Se presentó un error durante el procesamiento de la solicitud.</example>
    public string? Detail { get; set; }

    public MessageErrorProblem(string? message, string? detail)
    {
        Message = message;
        Detail = detail;
    }
}