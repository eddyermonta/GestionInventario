
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

        [HttpGet("{NIT}", Name = "GetSupplierByNIT")]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSupplierByNIT([FromRoute] string NIT)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            
            var supplier = _service.GetSupplierByNIT(NIT); 
            if (supplier == null) return NotFound(); // Devuelve 404 si no se encuentra el proveedor
            return Ok(supplier); // Devuelve 200 y el proveedor
        }

        [HttpGet(Name = "GetAllSuppliers")]
        [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _service.GetAllSuppliers();
            if (!suppliers.Any()) return NoContent(); // Devuelve 204 si no hay proveedores

            return Ok(suppliers); // Devuelve 200 y la lista de proveedores
        }

        [HttpPost (Name = "AddSupplier")]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddSupplier([FromBody] SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido

            _service.AddSupplier(supplierDto); // Añade el proveedor

            return CreatedAtRoute("GetSupplierByNIT", new { supplierDto.NIT }, supplierDto); // Devuelve 201 y el proveedor
        }

        [HttpPut("{NIT}", Name = "UpdateSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSupplier([FromRoute] string NIT, [FromBody] SupplierUpdateDto supplierUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _service.UpdateSupplier(supplierUpdateDto, NIT);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el proveedor

            return NoContent(); // Devuelve 204 si se ha actualizado correctamente
        }

        [HttpDelete("{NIT}",Name = "DeleteSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSupplierByNIT([FromRoute]  string NIT)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _service.DeleteSupplierByNIT(NIT);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el proveedor

            return NoContent(); // Devuelve 204 si se ha eliminado correctamente
        }
    }
}