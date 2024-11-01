using GestionInventario.src.Modules.Inventories.Domains.Models;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Products.Repositories;

namespace GestionInventario.src.Modules.Inventories.Services
{
    public class InventaryService
    (
        ProductRepository productRepository
    ) : IInventaryService
    {
        private readonly ProductRepository _productRepository = productRepository;
        private readonly Inventary _inventary = new();
        public void AddMovement(MovementDto movementDto)
        {
            _inventary.Movements.Add(movementDto.Id, movementDto);
        }

        public void FillInventary()
        {
            var products = _productRepository.GetAllProducts();
            foreach (var product in products)
            {
                _inventary.Movements.Add(product.Id, new MovementDto
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    Amount = product.Amount,
                    Date = DateTime.Now.ToString(),
                    Reason = "Initial stock"
                });
            }
        }

        public MovementDto? GetMovement(Guid productId)
        {
             return _inventary.Movements.TryGetValue(productId, out var movement) ? movement! : null;
        }
    }
}