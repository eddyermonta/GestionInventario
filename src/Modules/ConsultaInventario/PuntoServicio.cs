using System;
using System.Collections.Generic;
using System.Linq;

public class ProductoServicio
{
    private List<Producto> productos = new List<Producto>();
    private List<Movimiento> movimientos = new List<Movimiento>();

    // Método para agregar un producto
    public void AgregarProducto(Producto producto)
    {
        productos.Add(producto);
    }

    // Método para agregar un movimiento
    public void AgregarMovimiento(Movimiento movimiento)
    {
        movimientos.Add(movimiento);
    }

    // Método para calcular la cantidad disponible de un producto basándose en los movimientos
    public int ObtenerCantidadDisponible(int productoId)
    {
        var producto = productos.FirstOrDefault(p => p.Id == productoId);
        if (producto == null) return 0;

        int entradas = movimientos
            .Where(m => m.ProductoId == productoId && m.TipoMovimiento == "Entrada")
            .Sum(m => m.Cantidad);

        int salidas = movimientos
            .Where(m => m.ProductoId == productoId && m.TipoMovimiento == "Salida")
            .Sum(m => m.Cantidad);

        return producto.CantidadInicial + entradas - salidas;
    }

    // Consultar inventario por nombre
    public List<Producto> ConsultarPorNombre(string nombre)
    {
        return productos.Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // Consultar inventario por categoría
    public List<Producto> ConsultarPorCategoria(string categoria)
    {
        return productos.Where(p => p.Categoria.Contains(categoria, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // Mostrar detalles del inventario
    public void MostrarInventario()
    {
        foreach (var producto in productos)
        {
            int cantidadDisponible = ObtenerCantidadDisponible(producto.Id);
            Console.WriteLine($"Producto: {producto.Nombre}, Categoría: {producto.Categoria}, Cantidad Disponible: {cantidadDisponible}, Precio: {producto.PrecioUnitario}");
        }
    }
}
