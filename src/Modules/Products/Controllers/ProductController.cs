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
        
        /// <summary>
        ///   Adds a product to a supplier and assigns categories to the product and her movement 
        /// </summary>
        /// <param name="productRequest">
        ///  Object containing the information of the product to be added
        /// </param>
        /// <param name="NIT">
        ///  NIT of the supplier to which the product will be added
        /// </param>
        /// <returns>
        ///  Returns the added product
        /// </returns>
        /// <response code="201">Returns the added product</response>
        /// <response code="400">The supplier was not found</response>
        /// <response code="404">The product cannot be inserted</response>
        [HttpPost ("{NIT}",Name = "AddProduct")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddProduct([FromBody] ProductRequest productRequest, [FromRoute] string NIT)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            // Comprueba si el proveedor existe
            var supplierResponse = _supplierService.GetSupplierByNIT(NIT);
            if (supplierResponse == null) return NotFound(new { message = "Proveedor no encontrado." }); // Devuelve 404 si no se encuentra el proveedor
            
           // Añade el producto retorno idProducto para product category y el movimiento
            var productResponseId = _productService.AddProduct(productRequest, supplierResponse.Id); // Añade el producto
            if (productResponseId == null) return BadRequest(new { message = "Error al insertar el producto." });

            
            List<string> missingCategories = [];
            //obtener lista de categorias verificar si existe, si existe añadir los ids
            foreach (var categoryName in productRequest.Categories)
            {
                var categoryResponse = _categoryService.GetCategoryByName(categoryName);
                if (categoryResponse == null)
                {
                    missingCategories.Add(categoryName); //no se encuentra la categoria
                    continue;
                } 
                if(AddProductCategory(productResponseId.Id, categoryResponse.Id))
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

        private bool AddProductCategory(Guid productId, Guid categoryId)
        {
            var productCategoryRequest = new ProductCategoryRequest
            {
                ProductId = productId,
                CategoryId = categoryId
            };
            return _productCategoryService.AddProductCategory(productCategoryRequest);

        }

         /// <summary>
         ///  Updates a product
         /// </summary>
         /// <param name="name">
         ///  Name of the product to be updated
         /// </param>
         /// <param name="productResponse">
         ///  Object containing the information of the product to be updated
         /// </param>
         /// <returns>
         ///  Returns 204 if the product is updated correctly
         /// </returns>  
         /// <response code="204">Returns 204 if the product is updated correctly</response>
         /// <response code="400">The product was not found</response>
         /// <response code="404">The product cannot be updated</response> 
        [HttpPut("{name}",Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct([FromRoute] string name ,[FromBody] ProductResponse productResponse)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _productService.UpdateProduct(productResponse, name);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el producto

            return NoContent(); // Devuelve 204 si se actualiza correctamente
        }

        /// <summary>
        ///  deletes a product
        /// </summary>
        /// <param name="name">
        ///  Name of the product to be deleted
        /// </param>
        /// <returns>
        ///  Returns 204 if the product is deleted correctly
        /// </returns>
        /// <response code="204">Returns 204 if the product is deleted correctly</response>
        /// <response code="404">Returns 404 if the product is not found</response>
        /// <response code="400">Returns 400 if the model is not valid</response>

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