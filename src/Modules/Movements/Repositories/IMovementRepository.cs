using GestionInventario.src.Modules.Movements.Domains.Models;

namespace GestionInventario.src.Modules.Movements.Repositories
{
    public interface IMovementRepository
    {
        Task AddMovement(Movement movement);
        Task<IEnumerable<Movement>> GetMovementsByProductId(Guid id);
        Task<IEnumerable<Movement>> GetAllMovements();
    }
}