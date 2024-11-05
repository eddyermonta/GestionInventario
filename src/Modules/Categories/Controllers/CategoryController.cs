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

        /// <summary>
        ///   Gets all categories
        /// </summary>
        /// <returns>
        ///  List of categories
        /// </returns>
        /// <response code="200">Returns the list of categories</response>
        ///  <response code="204">No categories found</response>
        [HttpGet(Name = "GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseName>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            if (!categories.Any()) return NoContent(); // Devuelve 204 si no hay categoría

            return Ok(categories); // Devuelve 200 y la lista de categoría
        }

        /// <summary>
        ///  Gets a category by its name
        /// </summary>
        /// <param name="nameCategory">
        ///  Name of the category
        /// </param>
        /// <returns>
        ///  Category
        /// </returns>
        /// <response code="200">Returns the category</response>
        /// <response code="404">Category not found</response>
        /// <response code="400">Bad request</response>

        [HttpGet("{nameCategory}", Name = "GetCategoryByName")]
        [ProducesResponseType(typeof(CategoryResponseName), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCategoryByName([FromRoute] string nameCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var category = _categoryService.GetCategoryByName(nameCategory);
            if (category == null) return NotFound(); // Devuelve 404 si no se encuentra la categoría
            return Ok(category); // Devuelve 200 y la categoría
        }

        /// <summary>
        ///  Adds a list of categories
        /// </summary>
        /// <param name="categoryRequest">
        ///  List of categories
        /// </param>
        /// <returns>
        ///  List of added and existing categories
        /// </returns>
        /// <response code="201">Returns the list of added and existing categories</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Category not found</response>
        [HttpPost(Name = "AddCategories")]
        [ProducesResponseType(typeof(CategoryResponseName), StatusCodes.Status201Created)]
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

        /// <summary>
        ///  Updates a category
        /// </summary>
        /// <param name="categoryResponseName">
        ///  Category to update
        /// </param>
        /// <param name="name">
        ///  Name of the category
        ///</param>
        /// <returns>
        ///  category updated
        /// </returns>
        ///  <response code="204">Category updated</response>
        ///  <response code="404">Category not found</response>
        ///  <response code="400">Bad request</response>    
        [HttpPut("{name}",Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory([FromBody] CategoryResponseName categoryResponseName, [FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _categoryService.UpdateCategory(categoryResponseName, name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra la categoría

            return NoContent(); // Devuelve 204
        }
        
        /// <summary>
        ///  Elimina una categoría
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///  No content
        /// </returns>
        /// <response code="204">Category deleted</response>
        /// <response code="404">Category not found</response>
        /// <response code="400">Bad request</response>
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