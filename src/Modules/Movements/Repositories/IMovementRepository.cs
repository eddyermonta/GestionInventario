using GestionInventario.src.Modules.Movements.Domains.Models;

namespace GestionInventario.src.Modules.Movements.Repositories
{
    public interface IMovementRepository
    {
        Task Add(Movement movement);
        Task<Movement> Get(int id);
        Task<IEnumerable<Movement>> GetAll();
    }
}