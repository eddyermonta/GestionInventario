using AutoMapper;
using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Categories.Repositories;
using GestionInventario.src.Modules.Products.Domain.DTOs;

namespace GestionInventario.src.Modules.Categories.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<(List<CategoryResponseName> AddedCategories, List<string> ExistingCategories)> AddCategories(List<string> namesCategories)
        {
            var addedCategories = new List<CategoryResponseName>();
            var existingCategories = new List<string>();

            foreach(var name in namesCategories)
            {
                if (string.IsNullOrEmpty(name)) throw new InvalidOperationException("El nombre de la categoría no puede ser nulo o vacío.");
                
                if (await _categoryRepository.GetCategoryByName(name) != null)
                {
                    existingCategories.Add(name);
                    continue;
                }

                var category = new Category { Name = name };
                await _categoryRepository.CreateCategory(category);
                addedCategories.Add(_mapper.Map<CategoryResponseName>(category));
            }

            return (addedCategories, existingCategories);
        }
        

        public async Task<bool> DeleteCategory(string name)
        {
            var existingCategory = await _categoryRepository.GetCategoryByName(name);
            if (existingCategory == null) return false;
            await _categoryRepository.DeleteCategory(existingCategory);
            return true;
        }

        public async Task<IEnumerable<CategoryResponseName>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return _mapper.Map<IEnumerable<CategoryResponseName>>(categories);
        }

        public async Task<CategoryResponse?> GetCategoryByName(string name)
        {
            var category = await _categoryRepository.GetCategoryByName(name);
            if (category == null) return null;
            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryProductsResponse?> GetProductsByCategoryName(string categoryName)
        {
            var category = await _categoryRepository.GetProductsByCategoryName(categoryName);
            if (category == null) return new CategoryProductsResponse { Products = [] };
            
            var products = category
            .Select(_mapper.Map<ProductResponse>)
            .ToList();
            
            return new CategoryProductsResponse
            {
                Products = products
            };
        }

        public async Task<bool> UpdateCategory(CategoryResponseName categoryResponseName, string name)
        {
            var existingCategory = await _categoryRepository.GetCategoryByName(name);
            if (existingCategory == null) return false;
            _mapper.Map(categoryResponseName, existingCategory);
            await _categoryRepository.UpdateCategory(existingCategory);
            return true;
        }
    }
    }