using GestionInventario.src.Modules.ProductsManagement.Categories.Domain.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Categories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.ProductsManagement.Categories.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController
    (
        ICategoryService categoryService
    )
        : ControllerBase

    {
        private readonly ICategoryService _categoryService = categoryService;

        /// <summary>  Gets the products of a category. </summary>
        /// <param name="categoryName">  The name of the category. </param>
        /// <returns>  Successful search: 200 OK and the list of category products.</returns>
        /// <response code="404">Category not found or no products.</response>
        /// <response code="400">The category name is invalid.</response>
        /// <response code="200">Successful search: 200 OK and the list of category products.</response>

        [HttpGet("product/{categoryName}", Name = "GetProductsByCategoryName")]
        [ProducesResponseType(typeof(CategoryProductsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductsByCategoryName([FromRoute] string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return BadRequest("Category name cannot be empty."); // Devuelve 400 si el nombre de la categoría es inválido
        
            var categoryProductsResponse = await _categoryService.GetProductsByCategoryName(categoryName);

            if (categoryProductsResponse == null || categoryProductsResponse.Products == null || categoryProductsResponse.Products.Count == 0)
                return NotFound("no existe la categoria o no existe algun producto asociado a la categoria"); // Devuelve 404 si no se encuentra la categoría o no hay productos
            return Ok(categoryProductsResponse); // Devuelve 200 y la lista de productos
        }

        /// <summary> Gets all categories </summary>
        /// <returns> List of categories </returns>
        /// <response code="200">Returns the list of categories</response>
        ///  <response code="204">No categories found</response>
        
        [HttpGet(Name = "GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseName>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            if (!categories.Any()) return NoContent(); // Devuelve 204 si no hay categoría

            return Ok(categories); // Devuelve 200 y la lista de categoría
        }

        /// <summary>Gets a category by its name </summary>
        /// <param name="nameCategory"> Name of the category</param>
        /// <returns> Category</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="404">Category not found</response>
        /// <response code="400">Bad request</response>

        [HttpGet("{nameCategory}", Name = "GetCategoryByName")]
        [ProducesResponseType(typeof(CategoryResponseName), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoryByName([FromRoute] string nameCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var category = await _categoryService.GetCategoryByName(nameCategory);
            if (category == null) return NotFound(); // Devuelve 404 si no se encuentra la categoría
            return Ok(category); // Devuelve 200 y la categoría
        }

        /// <permission cref="System.Security.Claims.ClaimTypes.Role" name="ADMIN, AUXILIAR"></permission>
        /// <summary>Adds a list of categories </summary>
        /// <param name="categoryRequest"> List of categories </param>
        /// <returns> List of added and existing categories </returns>
        /// <response code="201">Returns the list of added and existing categories</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Category not found</response>
        [Authorize ( Roles = "ADMIN, AUXILIAR")]
        [HttpPost(Name = "AddCategories")]
        [ProducesResponseType(typeof(CategoryResponseName), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategories([FromBody] CategoryRequest categoryRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            try
            {
                var (addedCategories, existingCategories) = await _categoryService.AddCategories(categoryRequest.NamesCategories);
                return CreatedAtRoute("GetAllCategories", new {}, new { Added = addedCategories, Existing = existingCategories }); // Devuelve 201 y la lista de categorías añadidas y existentes
            
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message }); // Devuelve 400 si hay un problema de validación
            }
        }

        /// <permission cref="System.Security.Claims.ClaimTypes.Role" name="ADMIN"></permission>
        /// <summary> Updates a category </summary>
        /// <param name="categoryResponseName">  Category to update</param>
        /// <param name="name"> Name of the category</param>
        /// <returns> category updated</returns>
        ///  <response code="204">Category updated</response>
        ///  <response code="404">Category not found</response>
        ///  <response code="400">Bad request</response>  
        [Authorize (Roles = "ADMIN")]
        [HttpPut("{name}",Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryResponseName categoryResponseName, [FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = await _categoryService.UpdateCategory(categoryResponseName, name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra la categoría

            return NoContent(); // Devuelve 204
        }
        
        /// <permission cref="System.Security.Claims.ClaimTypes.Role" name="ADMIN"></permission>
        /// <summary>Elimina una categoría </summary>
        /// <param name="name"></param> <returns> No content </returns>
        /// <response code="204">Category deleted</response>
        /// <response code="404">Category not found</response>
        /// <response code="400">Bad request</response>
        [Authorize (Roles = "ADMIN")] 
        [HttpDelete("{name}",Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async  Task<IActionResult> DeleteCategory([FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = await _categoryService.DeleteCategory(name);
            if (!success) return NotFound("No se puede borrar categoria asociada a producto o no existe"); // Devuelve 404 si no se encuentra la categoría

            return NoContent(); // Devuelve 204
        }
    }
}