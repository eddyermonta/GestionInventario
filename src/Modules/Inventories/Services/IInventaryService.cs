
using GestionInventario.src.Modules.Movements.Domains.DTOs;

namespace GestionInventario.src.Modules.Inventories.Services
{
    public interface IInventaryService
    {
        void AddMovement(MovementDto movementDto);
        MovementDto? GetMovement(Guid productId);
        void FillInventary();
    }
}