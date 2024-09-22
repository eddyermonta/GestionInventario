namespace GestionInventario.Domain.Exceptions;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }
}
