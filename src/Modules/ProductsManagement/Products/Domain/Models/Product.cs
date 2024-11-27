
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models;
using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.Model;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.Models;
namespace GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required ICollection<ProductCategory> ProductCategories  { get; set; } = [];
        public  required int Initial_Amount { get; set; }
        public required decimal UnitPrice { get; set; } 
        public DateOnly? ExpirationDate { get; set; }
        public Mesurement? Weight { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        //foreign key for supplier        
        public Guid SupplierId { get; set; }
        public required Supplier Supplier { get; set; }
        public virtual ICollection<Movement> Movements { get; set; } = [];
    
    }
}