
using AutoMapper;
using GestionInventario.src.Modules.ProductsManagement.Products.Repositories;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.DTOs;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Repositories;


namespace GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Services{
    public class SupplierService(ISupplierRepository supplierRepository, IProductRepository productRepository, IMapper mapper): ISupplierService
    {

        private readonly ISupplierRepository _supplierRepository = supplierRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;


        public async Task AddSupplier(SupplierDto supplierDto)
        {
            if (await _supplierRepository.GetSupplierByNIT(supplierDto.NIT) != null) throw new InvalidOperationException("El proveedor ya existe.");
            if (await _supplierRepository.GetSupplierByName(supplierDto.Name) != null) throw new InvalidOperationException("El proveedor ya existe.");
            
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplierRepository.CreateSupplier(supplier);
        }

        public async Task<bool> DeleteSupplierByNIT(string NIT)
        {
            var existingSupplier = await _supplierRepository.GetSupplierByNIT(NIT);
            if (existingSupplier == null) return false;

            // Verificar si tiene productos asociados
            var hasAssociatedProducts = await _productRepository.AnyProductWithSupplierId(existingSupplier.Id);
            if (hasAssociatedProducts) return false;

            
            await _supplierRepository.DeleteSupplierByNIT(existingSupplier);
            return true;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliers()
        {
            var suppliers = await _supplierRepository.GetAllSuppliers();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierResponse?> GetSupplierByNIT(string NIT)
        {
            var supplier = await _supplierRepository.GetSupplierByNIT(NIT);
            if (supplier == null) return null;
            return _mapper.Map<SupplierResponse>(supplier);
        }

        public async Task<SupplierResponse?> GetSupplierById(string id){
            var supplier = await _supplierRepository.GetSupplierById(id);
            if (supplier == null) return null;
            return _mapper.Map<SupplierResponse>(supplier);
        }

        public async Task<bool> UpdateSupplier(SupplierUpdateDto supplierDto, string NIT)
        {
            var existingSupplier = await _supplierRepository.GetSupplierByNIT(NIT);
            if (existingSupplier == null) return false;
            _mapper.Map(supplierDto, existingSupplier);
            await _supplierRepository.UpdateSupplier(existingSupplier);
            return true;
        }
    }
}


