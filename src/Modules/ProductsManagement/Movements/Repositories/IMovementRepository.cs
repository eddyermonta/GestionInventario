using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Repositories
{
    public interface IMovementRepository
    {
        Task AddMovement(Movement movement);
        Task<IEnumerable<Movement>> GetMovementsByProductId(Guid id);
        Task<IEnumerable<Movement>> GetAllMovements();
    }
}