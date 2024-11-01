using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Services;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Services;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Inventories.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController
    (
        IProductService productService,
        ICategoryService categoryService,
        IMovementManualService movementManualService,
        IMovementSupplierService movementSupplierService
    )   :ControllerBase
    
    {
        private readonly IProductService _productService = productService; 
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IMovementManualService _movementManualService = movementManualService;
        private readonly IMovementSupplierService _movementSupplierService = movementSupplierService;

        [HttpGet("product/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProductByName([FromRoute] string name)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var product = _productService.GetProductByName(name);
            if (product == null) return NotFound(); // Devuelve 404 si no se encuentra el producto
            return Ok(product); // Devuelve 200 y el producto
        }

        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            if (!products.Any()) return NoContent(); // Devuelve 204 si no hay producto

            return Ok(products); // Devuelve 200 y la lista de producto
        }

        [HttpGet("{categoryName}", Name = "GetProductsByCategoryName")]
        [ProducesResponseType(typeof(CategoryProductsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProductsByCategoryName([FromRoute] string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest("Category name cannot be empty."); // Devuelve 400 si el nombre de la categoría es inválido
            }

            var categoryProductsDto = _categoryService.GetProductByName(categoryName);

            if (categoryProductsDto.Products == null || categoryProductsDto.Products.Count == 0)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la categoría o no hay productos
            }

            return Ok(categoryProductsDto); // Devuelve 200 y la lista de productos
        }

        [HttpPut("{MovementType}",Name = "UpdateInventory")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateInventory([FromRoute] string MovementType, [FromBody] MovementDto movementDto)
        {
            if(string.IsNullOrEmpty(MovementType)) return BadRequest("MovementType cannot be empty.");
            
            if(MovementType == "Manual"){
                _movementManualService.AddInventoryStock(movementDto);
                return Ok("Product updated by manual");
            }
            
            if(MovementType == "SupplierReceipt"){
                _movementSupplierService.UpdateBySupplierReceipt(movementDto);
                return Ok("Product updated by supplier receipt");
            } 
        
            return BadRequest("MovementType is invalid");
        }
    }
}