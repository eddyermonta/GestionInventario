using AutoMapper;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Products.Repositories;

namespace GestionInventario.src.Modules.Products.Services
{
    public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public void AddProduct(ProductDto productDto)
        {
            if (_productRepository.GetProductByName(productDto.Name) != null) throw new InvalidOperationException("El producto ya existe.");
            var product = _mapper.Map<Product>(productDto);
            _productRepository.CreateProduct(product);
        }

        public bool DeleteProduct(ProductDto productDto)
        {
            var existingProduct = _productRepository.GetProductByName(productDto.Name);
            if (existingProduct == null) return false;
           _productRepository.DeleteProduct(existingProduct);
           return true;
        }

        public ProductDto? GetProductByName(string name)
        {
            var product = _productRepository.GetProductByName(name);
            if (product == null) return null;
            return _mapper.Map<ProductDto>(product);
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var users = _productRepository.GetAllProducts();
            return _mapper.Map<IEnumerable<ProductDto>>(users);
        }

        public bool UpdateProduct(ProductDto productDto)
        {
            var existingProduct = _productRepository.GetProductByName(productDto.Name);
            if (existingProduct == null) return false;
            _productRepository.UpdateProduct(existingProduct);
            return true;
        }
    }
}