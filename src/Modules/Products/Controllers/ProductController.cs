
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Products.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        
        [HttpGet("{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            if (!products.Any()) return NoContent(); // Devuelve 204 si no hay producto

            return Ok(products); // Devuelve 200 y la lista de producto
        }


        [HttpPost (Name = "AddProduct")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            _productService.AddProduct(productDto); // Añade el producto

            return CreatedAtRoute("GetProductByName", new { name = productDto.Name }, productDto); // Devuelve 201 y el producto
        } 

        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _productService.UpdateProduct(productDto);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el producto

            return NoContent(); // Devuelve 204 si se actualiza correctamente
        }

        [HttpDelete(Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _productService.DeleteProduct(productDto);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el producto

            return NoContent(); // Devuelve 204 si se elimina correctamente
        }
    }
}