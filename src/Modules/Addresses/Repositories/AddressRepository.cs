using GestionInventario.src.Data;
using GestionInventario.src.Modules.Addresses.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.Addresses.Repositories
{
    public class AddressRepository(MyDbContext myDbContext) : IAddressRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;
        
        public async Task<IEnumerable<Address>> GetAllAddress()
        {
            return await _myDbContext.AddressesBD.ToListAsync();
        }

        public async Task<Address?> GetAddressByUserId(string userId)
        {
            return await _myDbContext.AddressesBD
                             .FirstOrDefaultAsync(address => address.User.Id == userId);
        }

        public Task<bool> UpdateAddress(Address address)
        {
            return Task.Run(() =>
            {
                _myDbContext.AddressesBD.Update(address);
                return true;
            });
        }
    }
}