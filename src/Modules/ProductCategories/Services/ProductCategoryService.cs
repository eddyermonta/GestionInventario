

using AutoMapper;
using GestionInventario.src.Modules.ProductCategories.Domain.DTOs;
using GestionInventario.src.Modules.ProductCategories.Domain.Model;
using GestionInventario.src.Modules.ProductCategories.Repositories;

namespace GestionInventario.src.Modules.ProductCategories.Services
{
    public class ProductCategoryService(IProductCategoryRepository productCategoryRepository, IMapper mapper) : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
        private readonly IMapper _mapper = mapper;
        
        public async Task<bool> AddProductCategory(ProductCategoryRequest productCategoryRequest)
        {
            var productCategory = _mapper.Map<ProductCategory>(productCategoryRequest);
            return await Task.Run(() => _productCategoryRepository.CreateProductCategory(productCategory));
        }
        
    }
}