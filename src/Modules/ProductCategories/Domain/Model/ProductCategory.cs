using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.ProductCategories.Domain.Model
{
    public class ProductCategory
    {
    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public Guid CategoryId { get; set; }
    public required Category Category { get; set; }
    }
}