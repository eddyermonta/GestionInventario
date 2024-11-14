using GestionInventario.src.Data;
using GestionInventario.src.Modules.Movements.Domains.Models;

namespace GestionInventario.src.Modules.Movements.Repositories
{
    public class MovementRepository
    (
        MyDbContext myDbContext
    ) 
        : IMovementRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;

        public async Task Add(Movement movement)
        {
            _myDbContext.MovementsBD.Add(movement);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task<Movement> Get(int id)
        {
            return await Task.Run(() => _myDbContext.MovementsBD.FirstOrDefault(m => m.Id.Equals(id))!);
        }

        public Task<IEnumerable<Movement>> GetAll()
        {
            return Task.FromResult<IEnumerable<Movement>>(_myDbContext.MovementsBD.ToList());
        }
    }
}