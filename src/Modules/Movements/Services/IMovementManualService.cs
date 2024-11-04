using GestionInventario.src.Modules.Movements.Domains.DTOs;

namespace GestionInventario.src.Modules.Movements.Services
{
    public interface IMovementManualService
    {
        MovementResponse? AddInventoryStock (MovementRequest movementRequest);
        MovementResponse? ReduceInventoryStock (MovementRequest movementRequest);
    }
}