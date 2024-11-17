using AutoMapper;
using GestionInventario.src.Data;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Categories.Repositories;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Domains.Models;
using GestionInventario.src.Modules.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.Movements.Repositories;
using GestionInventario.src.Modules.ProductCategories.Domain.Model;
using GestionInventario.src.Modules.ProductCategories.Repositories;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Products.Repositories;

namespace GestionInventario.src.Modules.Products.Services
{
    public class ProductService
    (
        IProductRepository productRepository,
        IProductCategoryRepository productCategoryRepository,
        ICategoryRepository categoryRepository,
        IMovementRepository movementRepository,
        IMapper mapper,
        MyDbContext context
    ) 
    : IProductService
    {

        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMovementRepository _movementRepository = movementRepository;
        private readonly IMapper _mapper = mapper;
        private readonly MyDbContext _context = context;

        public async Task<ProductResponseId?> AddProduct(ProductRequest productRequest, Guid supplierId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verificar si ya existe un producto con el mismo nombre ya que no se pueden repetir
                if (await _productRepository.GetProductByName(productRequest.Name) != null) return null;
                var product = _mapper.Map<Product>(productRequest);
               
                product.SupplierId = supplierId;

                await _productRepository.CreateProduct(product);
                await AddMovementReasonAsync(product, "Creación de producto");

                await transaction.CommitAsync();
                
                return _mapper.Map<ProductResponseId>(product);

            }catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> DeleteProduct(string name)
        {
            var existingProduct = await _productRepository.GetProductByName(name);
            if (existingProduct == null) return false;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _productRepository.DeleteProduct(existingProduct);
                await AddMovementReasonAsync(existingProduct, "Eliminación de producto");
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            
           
        }

        public async Task<ProductResponse?> GetProductByName(string name)
        {
            var product = await _productRepository.GetProductByName(name);
            if (product == null) return null;
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProducts()
        {
            var users = await _productRepository.GetAllProducts();
            return _mapper.Map<IEnumerable<ProductResponse>>(users);     
        }

        public async Task<bool> UpdateProduct(ProductUpdateRequest productUpdateRequest, string name)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingProduct = await _productRepository.GetProductByName(name);

                if (existingProduct == null) return false;

                await AddMovementReasonAsync(existingProduct, "Actualización de producto");
                await UpdateProductCategoriesAsync(existingProduct, productUpdateRequest.Categories);


                await _productRepository.UpdateProduct(existingProduct);
                await AddMovementReasonAsync(existingProduct, "Actualización de producto");
                
                await transaction.CommitAsync();
                return true;

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }    
        }

        private async Task UpdateProductCategoriesAsync(Product existingProduct, List<string> newCategoryNames)
        {
            // Obtener los nombres de las categorías actuales del producto
            var currentCategoryNames = existingProduct.ProductCategories.Select(pc => pc.Category.Name).ToList();

            // Eliminar categorías que ya no están en la nueva lista
            var categoriesToRemove = currentCategoryNames.Except(newCategoryNames).ToList();
            foreach (var categoryName in categoriesToRemove)
            {
                var productCategory = existingProduct.ProductCategories.First(pc => pc.Category.Name == categoryName);
                await _productCategoryRepository.Remove(productCategory); // Elimina la relación
            }

            // Agregar solo las nuevas categorías que no existen en la lista actual
            var categoriesToAdd = newCategoryNames.Except(currentCategoryNames).ToList();
            foreach (var categoryName in categoriesToAdd)
            {
                // Comprobar si la categoría ya existe en el repositorio
                var existingCategory = await _categoryRepository.GetCategoryByName(categoryName);
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

        private async Task AddMovementReasonAsync(Product product, string reason)
        {
             var Movementresponse = new MovementResponse(){
                Date = DateTime.UtcNow,
                CategoryMov = MovementCategory.entrada,
                Amount = product.Initial_Amount,
                UnitPrice = product.UnitPrice,
                Reason = reason,
                ProductName = product.Name
            };

            var movement = _mapper.Map<Movement>(Movementresponse);
            movement.ProductId = product.Id;
            movement.Product = product;
            
            await _movementRepository.Add(movement);     
        }

    }
}