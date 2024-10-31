
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

        [HttpGet("{name}", Name = "GetSupplierByName")]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSupplierByName([FromRoute] string name)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            
            var supplier = _service.GetSupplierByName(name); 
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

            return CreatedAtRoute("GetSupplierByName", new { name = supplierDto.Name }, supplierDto); // Devuelve 201 y el proveedor
        }

        [HttpPut(Name = "UpdateSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSupplier([FromBody] SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _service.UpdateSupplier(supplierDto);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el proveedor

            return NoContent(); // Devuelve 204 si se ha actualizado correctamente
        }

        [HttpDelete(Name = "DeleteSupplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSupplier([FromBody]  SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            var success = _service.DeleteSupplier(supplierDto);
            if (!success) return NotFound(); // Devuelve 404 si no se encuentra el proveedor

            return NoContent(); // Devuelve 204 si se ha eliminado correctamente
        }
    }
}