using AutoMapper;
using GestionInventario.src.Modules.Addresses.Domains.DTOS;
using GestionInventario.src.Modules.Addresses.Domains.Models;
using GestionInventario.src.Modules.Addresses.Repositories;
using GestionInventario.src.Modules.Users.Addresses.DTOS;


namespace GestionInventario.src.Modules.Addresses.Services
{
    public class AddressService (IAddressRepository addressRepository, IMapper mapper) : IAddressService
    {
        private readonly IAddressRepository _addressRepository = addressRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<AddressResponse?> GetAddressByUserId(string userId)
        {
           var address = await _addressRepository.GetAddressByUserId(userId);
           if (address == null) return null;
           return _mapper.Map<AddressResponse>(address);
        }

        public async Task<IEnumerable<AddressResponse>> GetAllAddress()
        {
            var addresses = await _addressRepository.GetAllAddress();
            return _mapper.Map<IEnumerable<AddressResponse>>(addresses);
        }

        public Task<bool> UpdateUser(AddressUpdateRequest addressUpdateRequest)
        {
            var address = _mapper.Map<Address>(addressUpdateRequest);
            return _addressRepository.UpdateAddress(address);
        }
    }
}