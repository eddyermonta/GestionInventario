using GestionInventario.src.Modules.Movements.Domains.DTOs;

namespace GestionInventario.src.Modules.Movements.Services
{
    public interface IMovementManualService
    {
        Task<MovementResponse?> AddInventoryStock (MovementRequest movementRequest);
        Task<MovementResponse?> ReduceInventoryStock (MovementRequest movementRequest);
    }
}