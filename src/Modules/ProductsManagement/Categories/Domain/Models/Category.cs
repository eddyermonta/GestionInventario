

using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.Model;

namespace GestionInventario.src.Modules.ProductsManagement.Categories.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; } = [];
    }
}