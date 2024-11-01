using AutoMapper;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Categories.Repositories;
using GestionInventario.src.Modules.ProductCategories.Domain.Model;
using GestionInventario.src.Modules.ProductCategories.Repositories;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Products.Repositories;

namespace GestionInventario.src.Modules.Products.Services
{
    public class ProductService(
        IProductRepository productRepository,
        IProductCategoryRepository productCategoryRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper
         ) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public ProductRequestDto AddProduct(ProductRequest productRequest, Guid supplierId)
        {
            //si el producto ya existe, lanza una excepción
            if (_productRepository.GetProductByName(productRequest.Name) != null) throw new InvalidOperationException("El producto ya existe.");
            
            var product = _mapper.Map<Product>(productRequest);
            product.SupplierId = supplierId;

            _productRepository.CreateProduct(product);
            
            return _mapper.Map<ProductRequestDto>(product);
        }

        public bool DeleteProduct(string name)
        {
            var existingProduct = _productRepository.GetProductByName(name);
            if (existingProduct == null) return false;
           _productRepository.DeleteProduct(existingProduct);
           return true;
        }

        public ProductResponse? GetProductByName(string name)
        {
            var product = _productRepository.GetProductByName(name);
            if (product == null) return null;
            return _mapper.Map<ProductResponse>(product);
        }

        public IEnumerable<ProductResponse> GetAllProducts()
        {
            var users = _productRepository.GetAllProducts();
            return _mapper.Map<IEnumerable<ProductResponse>>(users);
        }

        public bool UpdateProduct(ProductUpdateDto productUpdateDto, string name)
        {
            var existingProduct = _productRepository.GetProductByName(name);
            if (existingProduct == null) return false;

            _mapper.Map(productUpdateDto, existingProduct);
            UpdateProductCategories(existingProduct, productUpdateDto.CategoryNames);
            _productRepository.UpdateProduct(existingProduct);
            return true;
        }

        private void UpdateProductCategories(Product existingProduct, List<string> newCategoryNames)
        {
            // Obtener los nombres de las categorías actuales del producto
            var currentCategoryNames = existingProduct.ProductCategories.Select(pc => pc.Category.Name).ToList();

            // Eliminar categorías que ya no están en la nueva lista
            var categoriesToRemove = currentCategoryNames.Except(newCategoryNames).ToList();
            foreach (var categoryName in categoriesToRemove)
            {
                var productCategory = existingProduct.ProductCategories.First(pc => pc.Category.Name == categoryName);
                _productCategoryRepository.Remove(productCategory); // Elimina la relación
            }

            // Agregar solo las nuevas categorías que no existen en la lista actual
            var categoriesToAdd = newCategoryNames.Except(currentCategoryNames).ToList();
            foreach (var categoryName in categoriesToAdd)
            {
                // Comprobar si la categoría ya existe en el repositorio
                var existingCategory = _categoryRepository.GetCategoryByName(categoryName);
                if (existingCategory != null)
                {
                    // Agregar la categoría existente al producto
                    existingProduct.ProductCategories.Add(new ProductCategory { Category = existingCategory, Product = existingProduct });
                }
                else
                {
                    // Crear y agregar una nueva categoría si no existe
                    var newCategory = new Category { Name = categoryName };
                    existingProduct.ProductCategories.Add(new ProductCategory { Category = newCategory, Product = existingProduct });
                }
            }
        }

    }
}