using GestionInventario.src.Modules.Inventories.Services;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Products.Repositories;

namespace GestionInventario.src.Modules.Movements.Services
{
       public class MovementManualService
       (
        IProductRepository productRepository,
        IInventaryService inventaryService
       ) : IMovementManualService
       {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IInventaryService _inventaryService = inventaryService;

        public void AddInventoryStock(MovementDto movementDto)
        {
            var product = _productRepository.GetProductByName(movementDto.ProductName);
            product.Amount += movementDto.Amount;
            _productRepository.UpdateProduct(product);

            AddMovement(movementDto);
        }

        public void ReduceInventoryStock(MovementDto movementDto)
        {
             var product = _productRepository.GetProductByName(movementDto.ProductName);
            product.Amount -= movementDto.Amount;
            _productRepository.UpdateProduct(product);
            
            AddMovement(movementDto);
        }

        private void AddMovement(MovementDto movementDto)
        {
            _inventaryService.AddMovement(movementDto);
            
        }
    }
}