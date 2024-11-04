using GestionInventario.src.Modules.Movements.Domains.Models;

namespace GestionInventario.src.Modules.Movements.Repositories
{
    public interface IMovementRepository
    {
        void Add(Movement movement);
        Movement Get(int id);
        IEnumerable<Movement> GetAll();
    }
}