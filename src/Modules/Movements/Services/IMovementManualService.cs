using GestionInventario.src.Modules.Movements.Domains.DTOs;

namespace GestionInventario.src.Modules.Movements.Services
{
    public interface IMovementManualService
    {
        void AddInventoryStock(MovementDto movementDto);
        void ReduceInventoryStock(MovementDto movementDto);
    }
}