
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
            if (_supplierRepository.GetSupplierByNIT(supplierDto.NIT) != null) throw new InvalidOperationException("El proveedor ya existe.");
            if (_supplierRepository.GetSupplierByName(supplierDto.Name) != null) throw new InvalidOperationException("El proveedor ya existe.");
            
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _supplierRepository.CreateSupplier(supplier);
        }

        public bool DeleteSupplierByNIT(string NIT)
        {
            var existingSupplier = _supplierRepository.GetSupplierByNIT(NIT);
            if (existingSupplier == null) return false;
            _supplierRepository.DeleteSupplierByNIT(existingSupplier);
            return true;
        }

        public IEnumerable<SupplierDto> GetAllSuppliers()
        {
            var suppliers = _supplierRepository.GetAllSuppliers();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public SupplierGetElementDto? GetSupplierByNIT(string NIT)
        {
            var supplier = _supplierRepository.GetSupplierByNIT(NIT);
            if (supplier == null) return null;
            return _mapper.Map<SupplierGetElementDto>(supplier);
        }

        public bool UpdateSupplier(SupplierUpdateDto supplierDto, string NIT)
        {
            var existingSupplier = _supplierRepository.GetSupplierByNIT(NIT);
            if (existingSupplier == null) return false;
            _mapper.Map(supplierDto, existingSupplier);
            _supplierRepository.UpdateSupplier(existingSupplier);
            return true;
        }
    }
}


