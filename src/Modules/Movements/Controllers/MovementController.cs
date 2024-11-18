using AutoMapper;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.Movements.Services;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Products.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Movements.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController(
        IMovementManualService movementManualService,
        IProductService productService,
        IMapper mapper
        
        ) : ControllerBase
    {
        private readonly IMovementManualService _movementManualService = movementManualService;
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;
  
        /// <summary>
        /// Updates the inventory of products.
        /// </summary>
        /// <param name="movementForm">
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
        
        [HttpPut("{movementForm}", Name = "UpdateInventory")]
        [ProducesResponseType(typeof(ProductWithMovementsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInventory([FromRoute] MovementForm movementForm, [FromBody] MovementRequest movementRequest)
        {
            var movement = await _movementManualService.UpdateInventoryStock(movementRequest, movementForm);
            if(movement == null) return BadRequest("Movement not created"); 
            
            var product = await _productService.GetProductByName(movementRequest.ProductName);
            if (product == null) return BadRequest("Product not found");

            var productModel = _mapper.Map<Product>(product);
            var productWithMovements = await _movementManualService.GetMovementsByProductId(productModel);
            if (productWithMovements == null) return BadRequest("Unable to fetch product movements");

            // Responder con el ProductWithMovementsResponse
            return Ok(productWithMovements);
        } 

        /// <summary>
        ///   Gets all movements
        /// </summary>
        /// <returns>
        ///  List movements
        /// </returns>
        /// <response code="200">Returns the list of movements</response>
        ///  <response code="204">No movements found</response>
        [HttpGet(Name = "GetAllMovements")]
        [ProducesResponseType(typeof(IEnumerable<ProductWithMovementsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllMovements()
        {
            var movements = await _movementManualService.GetProductsWithMovements();
            if (movements == null || !movements.Any()) return NoContent(); // Devuelve 204 si no hay categoría

            return Ok(movements); // Devuelve 200 y la lista de categoría
        }


        /// <summary>
        /// Retrieves the movements of a product by its name.
        /// </summary>
        /// <param name="nameProduct">Name of the product</param>
        /// <returns>List of movements associated with the product</returns>
        /// <response code="200">Returns the list of movements</response>
        /// <response code="404">The product or its movements were not found</response>
        /// <response code="400">Invalid request</response>
        [HttpGet("{nameProduct}", Name = "GetMovementsByProductName")]
        [ProducesResponseType(typeof(IEnumerable<ProductWithMovementsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMovementsByProductName([FromRoute] string nameProduct)
        {
            // Validar el estado del modelo
            if (!ModelState.IsValid) 
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            // Buscar el producto por nombre
            var productResponse = await _productService.GetProductByName(nameProduct);
            if (productResponse == null) 
                return NotFound($"El producto con nombre '{nameProduct}' no fue encontrado."); // Devuelve 404 si no se encuentra el producto

            // Mapear a modelo de dominio si es necesario
            var product = _mapper.Map<Product>(productResponse);

            // Obtener los movimientos del producto
            var movements = await _movementManualService.GetMovementsByProductId(product);

            // Validar si hay movimientos
            if (movements == null || !movements.Any()) 
                return NotFound($"No se encontraron movimientos para el producto '{nameProduct}'."); // Devuelve 404 si no hay movimientos

            // Retornar movimientos con código 200
            return Ok(movements);
        }

        /// <summary>
        /// Retrieves the movements of a product by its name.
        /// </summary>
        /// <param name="idProduct">Id of the product</param>
        /// <returns>List of movements associated with the product</returns>
        /// <response code="200">Returns the list of movements</response>
        /// <response code="404">The product or its movements were not found</response>
        /// <response code="400">Invalid request</response>
        [HttpGet("/Movement/{idProduct}", Name = "GetMovementsByProductId")]
        [ProducesResponseType(typeof(IEnumerable<ProductWithMovementsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMovementsByProductId([FromRoute] Guid idProduct)
        {
            // Validar el estado del modelo
            if (!ModelState.IsValid) 
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            // Buscar el producto por nombre
            var productResponse = await _productService.GetProductById(idProduct);
            if (productResponse == null) 
                return NotFound($"El producto con nombre '{idProduct}' no fue encontrado."); // Devuelve 404 si no se encuentra el producto

            // Mapear a modelo de dominio si es necesario
            var product = _mapper.Map<Product>(productResponse);

            // Obtener los movimientos del producto
            var movements = await _movementManualService.GetMovementsByProductId(product);

            // Validar si hay movimientos
            if (movements == null || !movements.Any()) 
                return NotFound($"No se encontraron movimientos para el producto '{idProduct}'."); // Devuelve 404 si no hay movimientos

            // Retornar movimientos con código 200
            return Ok(movements);
        }

                   
    }
}

        