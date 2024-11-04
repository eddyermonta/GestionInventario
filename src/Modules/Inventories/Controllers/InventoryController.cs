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

        /// <param name="movementType">El tipo de movimiento (Manual o Automatic).</param>
        /// <param name="movementCategory">La categoría del movimiento (Entrada o Salida).</param>
        /// <param name="movementRequest">La solicitud de movimiento que contiene los detalles del movimiento.</param>
        [HttpPut("{movementType}",Name = "UpdateInventory")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateInventory(
            [FromRoute] MovementType movementType,
            [FromRoute] MovementCategory movementCategory,
            [FromBody] MovementRequest movementRequest)
        {
            if(movementType == MovementType.Manual){
                
                if(movementCategory == MovementCategory.entrada)
                {
                   var movement = _movementManualService.AddInventoryStock(movementRequest);
                   if(movement == null) return BadRequest("Movement not created"); 
                   return Ok("Movement created");

                }else if (movementCategory == MovementCategory.salida){

                    var movement = _movementManualService.ReduceInventoryStock(movementRequest);
                    if(movement == null) return BadRequest("Movement not created"); 
                    return Ok("Movement created");
                }
            } 
            
            if(movementType == MovementType.SupplierReceipt){
                var movement =_movementSupplierService.UpdateBySupplierReceipt(movementRequest);
                if(movement == null) return BadRequest("Movement not created"); 
                return Ok("Movement created");
            } 
        
            return BadRequest("MovementType is invalid");
        }

        /// <summary>
        /// Actualiza el inventario basado en un recibo de proveedor cargado desde un archivo Excel.
        /// </summary>
        /// <param name="file">El archivo Excel que contiene el recibo del proveedor.</param>
        /// <returns>Una respuesta indicando el resultado de la operación.</returns>
        [HttpPost("UpdateBySupplierReceipt")]
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