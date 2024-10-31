public class Movimiento
{
    public int ProductoId { get; set; }
    public DateTime Fecha { get; set; }
    public string TipoMovimiento { get; set; } // Entrada o Salida
    public int Cantidad { get; set; }
    public string Motivo { get; set; }

    // Constructor
    public Movimiento(int productoId, DateTime fecha, string tipoMovimiento, int cantidad, string motivo)
    {
        ProductoId = productoId;
        Fecha = fecha;
        TipoMovimiento = tipoMovimiento;
        Cantidad = cantidad;
        Motivo = motivo;
    }
}
