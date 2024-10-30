namespace GestionInventario.src.Products.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Descripcion { get; set; }
        public required string Categoria { get; set; }
        public  required string Cantidad { get; set; }
        public required string PrecioUnitario { get; set; } 
        public DateTime? FechaExpiracion { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
    }
}