using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.DTOs;

namespace GestionInventario.src.Modules.UsersRolesManagement.Addresses.Services
{
    public interface IAddressService
    {
        Task<bool> UpdateUser(AddressUpdateRequest addressUpdateRequest);
        Task<AddressResponse?> GetAddressByUserId(string userId);
        Task<IEnumerable<AddressResponse>>  GetAllAddress();
    }
}