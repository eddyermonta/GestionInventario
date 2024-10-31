using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Suppliers.Domains.Models;
namespace GestionInventario.src.Modules.Products.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required ICollection<ProductCategory> ProductCategories  { get; set; } = [];
        public  required int Amount { get; set; }
        public required decimal UnitPrice { get; set; } 
        public DateTime? ExpirationDate { get; set; }
        public UnitMeasurement UnitMeasurement { get; set; }
        
        //foreign key for supplier        
        public Guid SupplierId { get; set; }
        public required Supplier Supplier { get; set; }
    
    }
}