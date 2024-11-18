using GestionInventario.src.Data;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Repositories
{
    public class MovementRepository( MyDbContext myDbContext) : IMovementRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;

        public async Task AddMovement(Movement movement)
        {
            _myDbContext.MovementsBD.Add(movement);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movement>> GetMovementsByProductId(Guid id)
        {
            var movements = await Task.Run(() => 
            _myDbContext.MovementsBD.Where(m => m.ProductId == id).ToList());
            return movements;
        }

        public async Task<IEnumerable<Movement>> GetAllMovements()
        {
            var movements = await _myDbContext.MovementsBD.Include(m => m.Product).ToListAsync();
            return movements;
        }
    }
}