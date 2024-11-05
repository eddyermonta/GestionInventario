using AutoMapper;
using GestionInventario.src.Data;
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
        IMapper mapper,
        MyDbContext context
       ) : IMovementManualService
       {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMovementRepository _movementRepository = movementRepository;
        protected readonly IMapper _mapper = mapper;
        public MovementResponse? AddInventoryStock(MovementRequest movementRequest)
        {
                var product = _productRepository.GetProductByName(movementRequest.ProductName);
                if (product == null) return null;
            
                product.Amount += movementRequest.Amount;
                _productRepository.UpdateProduct(product); // actualiza la cantidad de productos  
                var movementResponse = AddMovement(product, movementRequest);

             return movementResponse;
        }
            

        public MovementResponse? ReduceInventoryStock(MovementRequest movementRequest)
        {
                var product = _productRepository.GetProductByName(movementRequest.ProductName);
                if (product == null) return null;
           
                if(product.Amount < movementRequest.Amount) return null; // si la cantidad de productos es menor a la cantidad que se quiere reducir, retorna null
                product.Amount -= movementRequest.Amount;
                _productRepository.UpdateProduct(product); // actualiza la cantidad de productos
                var movementResponse = AddMovement(product, movementRequest);  

                return movementResponse;     
        }

        private static MovementResponse ProductToMovementResponse(Product product, MovementRequest movementRequest)
        {
            return new MovementResponse
            {
                Date = DateTime.UtcNow,
                Amount = product.Amount,
                Reason = movementRequest.Reason,
                ProductName = product.Name,
            };
        }

        private MovementResponse AddMovement(Product product, MovementRequest movementRequest)
        {
            var movementResponse = ProductToMovementResponse(product, movementRequest); 
            var movement = _mapper.Map<Movement>(movementResponse);

            movement.ProductId = product.Id;
            movement.Product = product;
            _movementRepository.Add(movement);
            
            return movementResponse;
        }

    
    }
}