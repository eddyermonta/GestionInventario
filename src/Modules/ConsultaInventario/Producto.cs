public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public int CantidadInicial { get; set; }
    public decimal PrecioUnitario { get; set; }
    public DateTime? FechaExpiracion { get; set; } // Opcional

    // Constructor
    public Producto(int id, string nombre, string categoria, int cantidadInicial, decimal precioUnitario, DateTime? fechaExpiracion = null)
    {
        Id = id;
        Nombre = nombre;
        Categoria = categoria;
        CantidadInicial = cantidadInicial;
        PrecioUnitario = precioUnitario;
        FechaExpiracion = fechaExpiracion;
    }
}
