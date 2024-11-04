using GestionInventario.src.Data;
using GestionInventario.src.Modules.Movements.Domains.Models;

namespace GestionInventario.src.Modules.Movements.Repositories
{
    public class MovementRepository(MyDbContext myDbContext) : IMovementRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;

        public void Add(Movement movement)
        {
            _myDbContext.MovementsBD.Add(movement);
            _myDbContext.SaveChanges();
        }

        public Movement Get(int id)
        {
            return _myDbContext.MovementsBD.FirstOrDefault(m => m.Id.Equals(id))!;
        }

        public IEnumerable<Movement> GetAll()
        {
            return [.. _myDbContext.MovementsBD];
        }
    }
}