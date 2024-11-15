using GestionInventario.src.Modules.Addresses.Domains.DTOS;
using GestionInventario.src.Modules.Users.Addresses.DTOS;

namespace GestionInventario.src.Modules.Addresses.Services
{
    public interface IAddressService
    {
        Task<bool> UpdateUser(AddressUpdateRequest addressUpdateRequest);
        Task<AddressResponse?> GetAddressByUserId(string userId);
        Task<IEnumerable<AddressResponse>>  GetAllAddress();
    }
}