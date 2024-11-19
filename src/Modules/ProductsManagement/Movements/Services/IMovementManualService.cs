using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Services
{
    public interface IMovementManualService 
    {
        Task<MovementResponse?> UpdateInventoryStock (MovementRequest movementRequest, MovementForm movementForm);
        Task<IEnumerable<ProductWithMovementsResponse>?> GetMovementsByProductId(Product product);
        Task<IEnumerable<ProductWithMovementsResponse>?> GetProductsWithMovements();
        Task<byte[]> GenerateReport();
    }
}