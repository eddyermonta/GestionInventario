public class Consultas
{
    static void Main(string[] args)
    {
        ProductoServicio productoServicio = new ProductoServicio();

        // Agregar productos
        productoServicio.AgregarProducto(new Producto(1, "Café", "Bebida", 10, 5000));
        productoServicio.AgregarProducto(new Producto(2, "Azúcar", "Alimentos", 20, 3000));

        // Agregar movimientos
        productoServicio.AgregarMovimiento(new Movimiento(1, DateTime.Now, "Entrada", 5, "Nuevo proveedor"));
        productoServicio.AgregarMovimiento(new Movimiento(1, DateTime.Now, "Salida", 3, "Venta"));
        productoServicio.AgregarMovimiento(new Movimiento(2, DateTime.Now, "Salida", 5, "Consumo interno"));

        // Consultar inventario
        productoServicio.MostrarInventario();

        // Consultar por nombre
        var resultadoNombre = productoServicio.ConsultarPorNombre("Café");
        foreach (var producto in resultadoNombre)
        {
            Console.WriteLine($"Encontrado: {producto.Nombre}");
        }

        // Consultar por categoría
        var resultadoCategoria = productoServicio.ConsultarPorCategoria("Alimentos");
        foreach (var producto in resultadoCategoria)
        {
            Console.WriteLine($"Encontrado: {producto.Nombre}");
        }
    }
}
