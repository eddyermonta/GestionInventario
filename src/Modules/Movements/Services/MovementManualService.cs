using AutoMapper;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Domains.Models;
using GestionInventario.src.Modules.Movements.Repositories;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Products.Repositories;

namespace GestionInventario.src.Modules.Movements.Services
{
    public class MovementManualService
    (
        IProductRepository productRepository,
        IMovementRepository movementRepository,
        IMapper mapper
    ) 
        : IMovementManualService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMovementRepository _movementRepository = movementRepository;
        protected readonly IMapper _mapper = mapper;


        public async Task<MovementResponse?> AddInventoryStock(MovementRequest movementRequest)
        {
            var product = await _productRepository.GetProductByName(movementRequest.ProductName);
            if (product == null) return null;
        
            product.Initial_Amount += movementRequest.Amount;
            await _productRepository.UpdateProduct(product); // actualiza la cantidad de productos  
            var movementResponse = await AddMovement(product, movementRequest);

            return movementResponse;
        }
            

        public async Task<MovementResponse?> ReduceInventoryStock(MovementRequest movementRequest)
        {
            var product = await _productRepository.GetProductByName(movementRequest.ProductName);
            if (product == null) return null;
        
            if(product.Initial_Amount < movementRequest.Amount) return null; // si la cantidad de productos es menor a la cantidad que se quiere reducir, retorna null
            product.Initial_Amount -= movementRequest.Amount;
            await _productRepository.UpdateProduct(product); // actualiza la cantidad de productos
            var movementResponse = await AddMovement(product, movementRequest);  

            return movementResponse;     
        }

        private static MovementResponse ProductToMovementResponse(Product product, MovementRequest movementRequest)
        {
        return new MovementResponse
            {
                Date = DateTime.UtcNow,
                Amount = product.Initial_Amount,
                Reason = movementRequest.Reason,
                ProductName = product.Name,
            };
        }

        private async Task<MovementResponse> AddMovement(Product product, MovementRequest movementRequest)
        {
            var movementResponse = ProductToMovementResponse(product, movementRequest); 
            var movement = _mapper.Map<Movement>(movementResponse);

            movement.ProductId = product.Id;
            movement.Product = product;
            await _movementRepository.Add(movement);
            
            return movementResponse;
        }

    
    }
}