using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Movements.Services
{
    public interface IMovementManualService 
    {
        Task<MovementResponse?> UpdateInventoryStock (MovementRequest movementRequest, MovementForm movementForm);
        Task<IEnumerable<ProductWithMovementsResponse>> GetMovementsByProductId(Product product);
        Task<IEnumerable<ProductWithMovementsResponse>?> GetProductsWithMovements();

    }
}