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
        public (List<CategoryDto> AddedCategories, List<string> ExistingCategories) AddCategories(List<string> namesCategories)
        {
            var addedCategories = new List<CategoryDto>();
            var existingCategories = new List<string>();

            foreach(var name in namesCategories)
            {
                if (string.IsNullOrEmpty(name)) throw new InvalidOperationException("El nombre de la categoría no puede ser nulo o vacío.");
                
                if (_categoryRepository.GetCategoryByName(name) != null){
                    existingCategories.Add(name);
                    continue;
                }

                var category = new Category { Name = name };
                _categoryRepository.CreateCategory(category);
                addedCategories.Add(_mapper.Map<CategoryDto>(category));
            }

            return (addedCategories, existingCategories);
        }

        public bool DeleteCategory(string name)
        {
            var existingCategory = _categoryRepository.GetCategoryByName(name);
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

        public bool UpdateCategory(CategoryDto categoryDto, string name)
        {
            var existingCategory = _categoryRepository.GetCategoryByName(name);
            if (existingCategory == null) return false;
            _mapper.Map(categoryDto, existingCategory);
            _categoryRepository.UpdateCategory(existingCategory);
            return true;
        }
    }
}