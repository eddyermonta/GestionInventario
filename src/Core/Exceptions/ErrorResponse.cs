namespace GestionInventario.src.Core.Exceptions;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }
}
