using AutoMapper;
using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Categories.Repositories;

namespace GestionInventario.src.Modules.Categories.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;
        public void AddCategory(CategoryDto categoryDto)
        {
            if (_categoryRepository.GetCategoryByName(categoryDto.Name) != null) throw new InvalidOperationException("La categoría ya existe.");
            var category = _mapper.Map<Category>(categoryDto);
            _categoryRepository.CreateCategory(category);
        }

        public bool DeleteCategory(CategoryDto categoryDto)
        {
            var existingCategory = _categoryRepository.GetCategoryByName(categoryDto.Name);
            if (existingCategory == null) return false;
            _categoryRepository.DeleteCategory(existingCategory);
            return true;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public CategoryDto? GetCategoryByName(string name)
        {
            var category = _categoryRepository.GetCategoryByName(name);
            if (category == null) return null;
            return _mapper.Map<CategoryDto>(category);
        }

        public bool UpdateCategory(CategoryDto categoryDto)
        {
            var existingCategory = _categoryRepository.GetCategoryByName(categoryDto.Name);
            if (existingCategory == null) return false;
            _categoryRepository.UpdateCategory(existingCategory);
            return true;
        }
    }
}