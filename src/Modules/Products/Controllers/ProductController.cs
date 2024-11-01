using GestionInventario.src.Modules.Categories.Services;
using GestionInventario.src.Modules.ProductCategories.Domain.DTOs;
using GestionInventario.src.Modules.ProductCategories.Services;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Services;
using GestionInventario.src.Modules.Suppliers.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Products.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(
        IProductService productService,
        ISupplierService supplierService,
        IProductCategoryService productCategoryService,
        ICategoryService categoryService
        ) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IProductCategoryService _productCategoryService = productCategoryService; 
        private readonly ISupplierService _supplierService = supplierService;
        private readonly ICategoryService _categoryService = categoryService;
        

        
        [HttpGet("{name}", Name = "GetProductByName")]
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


        [HttpPost ("{NIT}",Name = "AddProduct")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddProduct([FromBody] ProductRequest productRequest, [FromRoute] string NIT)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            // Comprueba si el proveedor existe
            var supplier = _supplierService.GetSupplierByNIT(NIT);
            if (supplier == null) return NotFound(new { message = "Proveedor no encontrado." }); // Devuelve 404 si no se encuentra el proveedor
            
           // Añade el producto retorno idProducto para product category
            var productDto = _productService.AddProduct(productRequest, supplier.Id); // Añade el producto
            if (productDto == null) return BadRequest(new { message = "Error al insertar el producto." });

            
            List<string> missingCategories = [];
            //obtener lista de categorias verificar si existe, si existe añadir los ids
            foreach (var categoryName in productRequest.Categories)
            {
                var category = _categoryService.GetCategoryByName(categoryName);
                if (category == null)
                {
                    missingCategories.Add(categoryName); //no se encuentra la categoria
                    continue;
                } 
                AddProductCategory(productDto.Id, category.Id);
            }

            //mensaje categorias faltantes
            if(missingCategories.Count > 0)
            
                return CreatedAtRoute("GetProductByName", new { name = productDto.Name },
                    new { productDto, message = $"Producto creado. Las siguientes categorías no existen: {string.Join(", ", missingCategories)}." });
            
            return CreatedAtRoute("GetProductByName", new { name = productDto.Name }, productDto); // Devuelve 201 y el producto
        }

        private void AddProductCategory(Guid productId, Guid categoryId)
        {
            var productCategoryRequest = new ProductCategoryRequest
            {
                ProductId = productId,
                CategoryId = categoryId
            };
            _productCategoryService.AddProductCategory(productCategoryRequest);
        }

        [HttpPut("{name}",Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct([FromRoute] string name ,[FromBody] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _productService.UpdateProduct(productUpdateDto, name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el producto

            return NoContent(); // Devuelve 204 si se actualiza correctamente
        }

        [HttpDelete("{name}",Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct([FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _productService.DeleteProduct(name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el producto

            return NoContent(); // Devuelve 204 si se elimina correctamente
        }
    }
}