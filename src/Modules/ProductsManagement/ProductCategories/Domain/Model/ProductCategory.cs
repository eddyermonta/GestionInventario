using GestionInventario.src.Modules.ProductsManagement.Categories.Domain.Models;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;

namespace GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.Model
{
    public class ProductCategory
    {
    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public Guid CategoryId { get; set; }
    public required Category Category { get; set; }
    }
}