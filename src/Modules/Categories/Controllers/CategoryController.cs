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

        [HttpPost(Name = "AddCategories")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCategories([FromBody] CategoryRequest categoryRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            try
            {
                var (addedCategories, existingCategories) = _categoryService.AddCategories(categoryRequest.NamesCategories);
                return CreatedAtRoute("GetAllCategories", new {}, new { Added = addedCategories, Existing = existingCategories }); // Devuelve 201 y la lista de categorías añadidas y existentes
            
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message }); // Devuelve 400 si hay un problema de validación
            }
        }

        [HttpPut("{name}",Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory([FromBody] CategoryDto categoryDto, [FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _categoryService.UpdateCategory(categoryDto, name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra la categoría

            return NoContent(); // Devuelve 204
        }
        
        [HttpDelete("{name}",Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCategory([FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _categoryService.DeleteCategory(name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra la categoría

            return NoContent(); // Devuelve 204
        }
    }
}