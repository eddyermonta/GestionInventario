using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Categories.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet("{name}", Name = "GetCategoryByName")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCategoryByName([FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var category = _categoryService.GetCategoryByName(name);
            if (category == null) return NotFound(); // Devuelve 404 si no se encuentra la categoría
            return Ok(category); // Devuelve 200 y la categoría
        }

        [HttpGet(Name = "GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            if (!categories.Any()) return NoContent(); // Devuelve 204 si no hay categoría

            return Ok(categories); // Devuelve 200 y la lista de categoría
        }

        [HttpPost(Name = "AddCategory")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            _categoryService.AddCategory(categoryDto); // Añade la categoría

            return CreatedAtRoute("GetCategoryByName", new { name = categoryDto.Name }, categoryDto); // Devuelve 201 y la categoría
        }

        [HttpPut(Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _categoryService.UpdateCategory(categoryDto);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra la categoría

            return NoContent(); // Devuelve 204
        }
        
        [HttpDelete(Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _categoryService.DeleteCategory(categoryDto);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra la categoría

            return NoContent(); // Devuelve 204
        }
    }
}