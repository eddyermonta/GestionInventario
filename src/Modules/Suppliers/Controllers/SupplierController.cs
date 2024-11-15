
using GestionInventario.src.Modules.Suppliers.Domains.DTOs;
using GestionInventario.src.Modules.Suppliers.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Suppliers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController(ISupplierService supplierService) : ControllerBase
    {
        private readonly ISupplierService _service = supplierService;

        
        /// <summary>
        /// Gets a supplier by its NIT
        /// </summary>
        /// <param name="NIT">
        ///  The NIT of the supplier to search for.
        /// </param>
        /// <returns>
        ///  Successful search: 200 OK and the found supplier.
        /// </returns> 
        /// <response code="404">Supplier not found.</response>
        ///  <response code="400">The NIT is invalid.</response>
        ///  <response code="200">Successful search: 200 OK and the found supplier.</response> 
        [HttpGet("{NIT}", Name = "GetSupplierByNIT")]
        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSupplierByNIT([FromRoute] string NIT)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            
            var supplier = await _service.GetSupplierByNIT(NIT); 
            if (supplier == null) return NotFound(); // Devuelve 404 si no se encuentra el proveedor
            return Ok(supplier); // Devuelve 200 y el proveedor
        }
        

        [HttpGet("supplierId/{id}", Name = "GetSupplierById")]
        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSupplierById([FromRoute] string id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            
            var supplier = await _service.GetSupplierById(id); 
            if (supplier == null) return NotFound(); // Devuelve 404 si no se encuentra el proveedor
            return Ok(supplier); // Devuelve 200 y el proveedor
        }



        /// <summary>
        ///  Gets all suppliers
        /// </summary>
        /// <returns>
        /// List of suppliers
        /// </returns>
        ///  <response code="200">Returns the list of suppliers</response>
        ///  <response code="204">No suppliers found</response>

        [HttpGet(Name = "GetAllSuppliers")]
        [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _service.GetAllSuppliers();
            if (!suppliers.Any()) return NoContent(); // Devuelve 204 si no hay proveedores

            return Ok(suppliers); // Devuelve 200 y la lista de proveedores
        }

        /// <summary>
        ///  Adds a supplier
        /// </summary>
        /// <param name="supplierDto">
        ///  Supplier to add
        /// </param>
        /// <returns>
        ///  Supplier added
        /// </returns>
        /// <response code="201">Returns the added supplier</response>
        ///  <response code="400">Bad request</response>

        [HttpPost (Name = "AddSupplier")]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            await _service.AddSupplier(supplierDto); // Añade el proveedor

            return CreatedAtRoute("GetSupplierByNIT", new { supplierDto.NIT }, supplierDto); // Devuelve 201 y el proveedor
        }

        /// <summary>
        ///  Updates a supplier
        /// </summary>
        /// <param name="NIT">
        /// The NIT of the supplier to update. 
        /// </param>
        /// <param name="supplierUpdateDto">
        ///  Supplier to update
        /// </param>
        /// <returns>
        ///  Successful update: 204 No Content
        /// </returns>
        /// <response code="404">Supplier not found.</response>
        /// <response code="400">Bad request</response>
        /// <response code="204">Successful update: 204 No Content</response>
        [HttpPut("{NIT}", Name = "UpdateSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSupplier([FromRoute] string NIT, [FromBody] SupplierUpdateDto supplierUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = await _service.UpdateSupplier(supplierUpdateDto, NIT);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el proveedor

            return NoContent(); // Devuelve 204 si se ha actualizado correctamente
        }

        /// <summary>
        ///  Deletes a supplier
        /// </summary>
        /// <param name="NIT"> 
        ///  The NIT of the supplier to delete.
        /// </param>
        /// <returns>
        ///  Successful delete: 204 No Content
        /// </returns>
        /// <response code="404">Supplier not found.</response>
        /// <response code="400">Bad request</response>
        ///  <response code="204">Successful delete: 204 No Content</response> 
        [HttpDelete("{NIT}",Name = "DeleteSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSupplierByNIT([FromRoute]  string NIT)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = await _service.DeleteSupplierByNIT(NIT);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el proveedor

            return NoContent(); // Devuelve 204 si se ha eliminado correctamente
        }
    }
}