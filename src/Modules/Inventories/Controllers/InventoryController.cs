using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Services;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.Movements.Services;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

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


        /// <summary>
        /// Gets a product by its name.
        /// </summary>
        /// <param name="name">
        ///   The name of the product to search for.
        /// </param>
        /// <returns>
        ///  Successful search: 200 OK and the found product.
        /// </returns>
        /// <response code="404">Product not found.</response> 
        /// <response code="400">The product name is invalid.</response> 
        /// <response code="200">Successful search: 200 OK and the found product.</response>
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


        /// <summary>
        ///  Gets all products.
        /// </summary>
        /// <returns>
        ///  Successful search: 200 OK and the list of products.
        /// </returns>
        /// <response code="204">No products found.</response> 
        /// <response code="200">Successful search: 200 OK and the list of products.</response>
        
        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            if (!products.Any()) return NoContent(); // Devuelve 204 si no hay producto

            return Ok(products); // Devuelve 200 y la lista de producto
        }

        /// <summary>
        ///  Gets the products of a category.
        /// </summary>
        /// <param name="categoryName">
        ///  The name of the category.
        /// </param>
        /// <returns>
        ///  Successful search: 200 OK and the list of category products.
        /// </returns>
        /// <response code="404">Category not found or no products.</response>
        /// <response code="400">The category name is invalid.</response>
        /// <response code="200">Successful search: 200 OK and the list of category products.</response>

        [HttpGet("{categoryName}", Name = "GetProductsByCategoryName")]
        [ProducesResponseType(typeof(CategoryProductsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProductsByCategoryName([FromRoute] string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return BadRequest("Category name cannot be empty."); // Devuelve 400 si el nombre de la categoría es inválido
        
            var categoryProductsResponse = _categoryService.GetProductsByCategoryName(categoryName);

            if (categoryProductsResponse == null || categoryProductsResponse.Products == null || categoryProductsResponse.Products.Count == 0)
                return NotFound(); // Devuelve 404 si no se encuentra la categoría o no hay productos
            return Ok(categoryProductsResponse); // Devuelve 200 y la lista de productos
        }

        /// <summary>
        /// Updates the inventory of products.
        /// </summary>
        /// <param name="movementCategory">
        ///  The category of the movement (entry or exit).
        ///  </param>
        ///  <param name="movementRequest">
        ///  The information of the movement.
        ///   </param>
        ///  <returns>
        ///  Successful update: 200 OK and a success message.
        ///  </returns>
        ///  <response code="400">The movement type is invalid.</response>
        ///  <response code="200">Successful update: 200 OK and a success message.</response>
        
        [HttpPut("{movementCategory}", Name = "UpdateInventory")]
        [ProducesResponseType(typeof(MovementResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateInventory([FromRoute] MovementCategory movementCategory, [FromBody] MovementRequest movementRequest)
        {
            if(movementCategory == MovementCategory.entrada)
                {
                   var movement = _movementManualService.AddInventoryStock(movementRequest);
                   if(movement == null) return BadRequest("Movement not created"); 

                }
            if (movementCategory == MovementCategory.salida){
                    var movement = _movementManualService.ReduceInventoryStock(movementRequest);
                    if(movement == null) return BadRequest("Movement not created"); 
                    
                }
                return Ok("Movement created");
        } 
            
       /// <summary>
       ///  Updates the inventory of products by supplier receipt.
       /// </summary>
       /// <param name="file">
       ///  The Excel file with the supplier receipt information.
       /// </param>
       /// <returns>
       /// Successful update: 200 OK and a success message.
       /// </returns>
       /// <response code="400">The file is required.</response>
       /// <response code="200">Successful update: 200 OK and a success message.</response>
       
        [HttpPut("{file}",Name = "UpdateBySupplierReceipt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateByReceipt(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("El archivo es requerido.");

            try
            {
                using var stream = new MemoryStream();
                file.CopyTo(stream);
                stream.Position = 0;

                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets[0];

                var receiptRequest = new SupplierReceiptRequest
                {
                    SupplierId = Guid.Parse(worksheet.Cells[1, 2].Text),
                    Items = new List<SupplierReceiptItem>()
                };

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var item = new SupplierReceiptItem
                    {
                        ProductId = Guid.Parse(worksheet.Cells[row, 1].Text),
                        Quantity = int.Parse(worksheet.Cells[row, 2].Text)
                    };
                    receiptRequest.Items.Add(item);
                }

                //_movementSupplierService.UpdateBySupplierReceipt(receiptRequest);

                return Ok("Inventario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al procesar el archivo: {ex.Message}");
            }
        }
    }
}