
using AutoMapper;
using GestionInventario.src.Modules.Suppliers.Domains.DTOs;
using GestionInventario.src.Modules.Suppliers.Domains.Models;
using GestionInventario.src.Modules.Suppliers.Repositories;

namespace GestionInventario.src.Modules.Suppliers.Services{
    public class SupplierService(ISupplierRepository supplierRepository, IMapper mapper) : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository = supplierRepository;
        private readonly IMapper _mapper = mapper;

        public void AddSupplier(SupplierDto supplierDto)
        {
            if (_supplierRepository.GetSupplierByName(supplierDto.Name) != null) throw new InvalidOperationException("El proveedor ya existe.");
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _supplierRepository.CreateSupplier(supplier);
        }

        public bool DeleteSupplier(SupplierDto supplierDto)
        {
            var existingSupplier = _supplierRepository.GetSupplierByName(supplierDto.Name);
            if (existingSupplier == null) return false;
            _supplierRepository.DeleteSupplier(existingSupplier);
            return true;
        }

        public IEnumerable<SupplierDto> GetAllSuppliers()
        {
            var suppliers = _supplierRepository.GetAllSuppliers();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public SupplierDto? GetSupplierByName(string name)
        {
            var supplier = _supplierRepository.GetSupplierByName(name);
            if (supplier == null) return null;
            return _mapper.Map<SupplierDto>(supplier);
        }

        public bool UpdateSupplier(SupplierDto supplierDto)
        {
            var existingSupplier = _supplierRepository.GetSupplierByName(supplierDto.Name);
            if (existingSupplier == null) return false;
            _supplierRepository.UpdateSupplier(existingSupplier);
            return true;
        }
    }
}


