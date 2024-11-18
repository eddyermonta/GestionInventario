using AutoMapper;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.ProductsManagement.Movements.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;
using GestionInventario.src.Modules.ProductsManagement.Products.Repositories;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Services
{
    public class MovementManualService(IProductRepository productRepository, IMovementRepository movementRepository, 
        IKardexCalculators kardexCalculators,
        IMapper mapper) : IMovementManualService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMovementRepository _movementRepository = movementRepository;
        private readonly IKardexCalculators _kardexCalculators = kardexCalculators;
        protected readonly IMapper _mapper = mapper;


        public async Task<MovementResponse?> UpdateInventoryStock(MovementRequest movementRequest, MovementForm movementForm)
        {
            
            var product = await _productRepository.GetProductByName(movementRequest.ProductName);
            if (product == null) return null; 
            
            var movementResponse = await AddMovement(product, movementRequest, movementForm);
            return movementResponse;
        }

        private async Task<MovementResponse> AddMovement(Product product, MovementRequest movementRequest, MovementForm movementForm)
        {

            var movementResponse = _mapper.Map<MovementResponse>(movementRequest);

            movementResponse.ProductId = product.Id;
            movementResponse.CategoryMov = movementForm;

            var movements = await _movementRepository.GetMovementsByProductId(product.Id);
            var movementResponses = _mapper.Map<IEnumerable<MovementResponse>>(movements).OrderBy(m => m.ProductId);

            var ActualAmount = _kardexCalculators.FinalAmount(movementResponses);

            if(movementForm == MovementForm.salida && movementRequest.Amount > ActualAmount) return null;
            
            var movement = _mapper.Map<Movement>(movementResponse);          
            await _movementRepository.AddMovement(movement);
            
            return movementResponse;
        }
       

        public async Task<IEnumerable<ProductWithMovementsResponse>> GetMovementsByProductId(Product product)
        {
             var movements = await _movementRepository.GetMovementsByProductId(product.Id);
             var movementResponse = _mapper.Map<IEnumerable<MovementResponse>>(movements).OrderBy(m => m.ProductId);
             
             return [CreateProductWithMovementsResponse(product, movementResponse)];
        }

        public async Task<IEnumerable<ProductWithMovementsResponse>?> GetProductsWithMovements()
        {
            // Obtener todos los productos
            var products = await _productRepository.GetAllProducts();
            if (products == null) return null;

            // Obtener todos los movimientos
            var allMovements = await _movementRepository.GetAllMovements();
            if (allMovements == null) return null;

            // Agrupar movimientos por ProductId
            var movementsGroupedByProduct = allMovements.GroupBy(m => m.ProductId);
       

            // Mapear productos y sus movimientos
            var result = products.OrderBy(p => p.Name).Select(product =>
              {
                var movementsResponse = GetMovementsResponse(movementsGroupedByProduct, product);
                return CreateProductWithMovementsResponse(product, movementsResponse);
            });

            return result;
        }

        private IEnumerable<MovementResponse> GetMovementsResponse( IEnumerable<IGrouping<Guid?, Movement>> movementsGroupedByProduct,  Product product){
            var movements = movementsGroupedByProduct.FirstOrDefault(g => g.Key == product.Id)?.ToList() ?? [];
            return _mapper.Map<IEnumerable<MovementResponse>>(movements);
        }

        private ProductWithMovementsResponse CreateProductWithMovementsResponse(Product product, IEnumerable<MovementResponse> movementsResponse)
        {
            return new ProductWithMovementsResponse
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Movements = movementsResponse,

                //cantidad de ventas
                SalesAmount = _kardexCalculators.SumSales(movementsResponse,MovementForm.salida),
                //diferencia entre entradas y salidas
                FinalAmount = _kardexCalculators.FinalAmount(movementsResponse),
                //total de cantidad unitaria * precio unitario
                TotalPurchaseBalance = _kardexCalculators.TotalPurchaseBalance(movementsResponse),
                //total saldo promedio * (diferencia entre entradas y salidas)
                FinalBalance = _kardexCalculators.FinalBalance(
                    _kardexCalculators.FinalAmount(movementsResponse),
                     _kardexCalculators.AverageBalance(
                        _kardexCalculators.SumSales(movementsResponse,MovementForm.entrada)
                        ,_kardexCalculators.TotalPurchaseBalance(movementsResponse))),

            };
        }
    }
}