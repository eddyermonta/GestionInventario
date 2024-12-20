using System.Globalization;
using GestionInventario.src.Modules.ProductsManagement.Categories.Services;
using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.DTOs;
using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Services;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Products.Services;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.ProductsManagement.Products.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService, ISupplierService supplierService, IProductCategoryService productCategoryService, ICategoryService categoryService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IProductCategoryService _productCategoryService = productCategoryService; 
        private readonly ISupplierService _supplierService = supplierService;
        private readonly ICategoryService _categoryService = categoryService;
        

        /// <summary> Gets a product by its name.</summary>
        /// <param name="name"> The name of the product to search for.</param>
        /// <returns>  Successful search: 200 OK and the found product.</returns>
        /// <response code="404">Product not found.</response> 
        /// <response code="400">The product name is invalid.</response> 
        /// <response code="200">Successful search: 200 OK and the found product.</response>
        
        [HttpGet("product/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var product = await _productService.GetProductByName(name);
            if (product == null) return NotFound(); // Devuelve 404 si no se encuentra el producto
            return Ok(product); // Devuelve 200 y el producto
        }


        /// <permission cref="System.Security.Claims.ClaimTypes.Role">AUXILIAR, ADMIN</permission>
        /// <summary> Adds a product to a supplier and assigns categories to the product and her movement  </summary>
        /// <param name="productRequest">Object containing the information of the product to be added </param>
        /// <param name="NIT">  NIT of the supplier to which the product will be added </param>
        /// <returns> Returns the added product </returns>
        /// <response code="201">Returns the added product</response>
        /// <response code="400">The supplier was not found</response>
        /// <response code="404">The product cannot be inserted</response>
         
        [Authorize(Roles = "ADMIN, AUXILIAR")]
        [HttpPost ("{NIT}",Name = "AddProduct")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest, [FromRoute] string NIT)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            // Comprueba si el proveedor existe
            var supplierResponse = await _supplierService.GetSupplierByNIT(NIT);
            if (supplierResponse == null) return NotFound(new { message = "Proveedor no encontrado." }); // Devuelve 404 si no se encuentra el proveedor
            
           // Añade el producto retorno idProducto para product category y el movimiento
            var productResponseId = await _productService.AddProduct(productRequest, supplierResponse.Id); // Añade el producto
            if (productResponseId == null) return BadRequest(new { message = "Error al insertar el producto." });

            
            List<string> missingCategories = [];
            //obtener lista de categorias verificar si existe, si existe añadir los ids
            foreach (var categoryName in productRequest.Categories)
            {
                var categoryResponse = await _categoryService.GetCategoryByName(categoryName);
                if (categoryResponse == null)
                {
                    missingCategories.Add(categoryName); //no se encuentra la categoria
                    continue;
                } 
                if(await AddProductCategory(productResponseId.Id, categoryResponse.Id))
                {
                    productResponseId.Categories.Add(categoryName);
                }
            }
            //mensaje categorias faltantes
            if(missingCategories.Count > 0)
            
                return CreatedAtRoute("GetProductByName", new { name = productResponseId.Name },
                    new { productResponseId, message = $"Producto creado. Las siguientes categorías no existen: {string.Join(", ", missingCategories)}." });
            
            return CreatedAtRoute("GetProductByName", new { name = productResponseId.Name }, productResponseId); // Devuelve 201 y el producto
        }

        private async Task<bool> AddProductCategory(Guid productId, Guid categoryId)
        {
            var productCategoryRequest = new ProductCategoryRequest
            {
                ProductId = productId,
                CategoryId = categoryId
            };
            return await _productCategoryService.AddProductCategory(productCategoryRequest);

        }
        /// <permission cref="System.Security.Claims.ClaimTypes.Role">ADMIN</permission>
        /// <summary> Updates a product</summary>
        /// <param name="name"> Name of the product to be updated </param>
        /// <param name="productUpdateRequest"> Object containing the information of the product to be updated </param>
        /// <returns> Returns 204 if the product is updated correctly </returns>  
        /// <response code="204">Returns 204 if the product is updated correctly</response>
        /// <response code="400">The product was not found</response>
        /// <response code="404">The product cannot be updated</response> 
        
        [Authorize(Roles = "ADMIN")]
        [HttpPut("{name}",Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute] string name ,[FromBody] ProductUpdateRequest productUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = await _productService.UpdateProduct(productUpdateRequest, name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el producto

            return NoContent(); // Devuelve 204 si se actualiza correctamente
        }

        /// <permission cref="System.Security.Claims.ClaimTypes.Role">ADMIN</permission>
        /// <summary> deletes a product </summary>
        /// <param name="name">  Name of the product to be deleted </param>
        /// <returns> Returns 204 if the product is deleted correctly</returns>
        /// <response code="204">Returns 204 if the product is deleted correctly</response>
        /// <response code="404">Returns 404 if the product is not found</response>
        /// <response code="400">Returns 400 if the model is not valid</response>

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{name}",Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromRoute] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = await _productService.DeleteProduct(name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el producto

            return NoContent(); // Devuelve 204 si se elimina correctamente
        }

        /// <summary> Gets all products.</summary>
        /// <returns>  Successful search: 200 OK and the list of products.</returns>
        /// <response code="204">No products found.</response> 
        /// <response code="200">Successful search: 200 OK and the list of products.</response>
        
        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            if (!products.Any()) return NoContent(); // Devuelve 204 si no hay producto

            return Ok(products); // Devuelve 200 y la lista de producto
        }
    }
}