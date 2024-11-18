

using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.Models;

namespace GestionInventario.src.Modules.UsersRolesManagement.Addresses.Repositories
{
    public interface IAddressRepository
    {
        Task<bool> UpdateAddress(Address address);
        Task<Address?> GetAddressByUserId(string userId);
        Task<IEnumerable<Address>> GetAllAddress();
        
    }
}