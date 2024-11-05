using GestionInventario.src.Modules.Movements.Domains.DTOs;

namespace GestionInventario.src.Modules.Movements.Services
{
    public interface IMovementSupplierService
    {
        void UpdateBySupplierReceipt(IFormFile formFile);
    }
}