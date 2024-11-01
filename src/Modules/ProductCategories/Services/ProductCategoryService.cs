

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
        
        public void AddProductCategory(ProductCategoryRequest productCategoryRequest)
        {
            var productCategory = _mapper.Map<ProductCategory>(productCategoryRequest);
            _productCategoryRepository.CreateProductCategory(productCategory);
        }
        
    }
}