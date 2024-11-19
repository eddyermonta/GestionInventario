using System.Globalization;
using AutoMapper;
using CsvHelper;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.ProductsManagement.Movements.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;
using GestionInventario.src.Modules.ProductsManagement.Products.Repositories;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Services
{
    public class MovementManualService(IProductRepository productRepository, IMovementRepository movementRepository, IKardexCalculators kardexCalculators,IMapper mapper) : IMovementManualService
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

        private async Task<MovementResponse?> AddMovement(Product product, MovementRequest movementRequest, MovementForm movementForm)
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
       

        public async Task<IEnumerable<ProductWithMovementsResponse>?> GetMovementsByProductId(Product product)
        {
             var movements = await _movementRepository.GetMovementsByProductId(product.Id);
             var movementResponse = _mapper.Map<IEnumerable<MovementResponse>>(movements).OrderBy(m => m.ProductId);
             
             return [CreateProductWithMovementsResponse(product, movementResponse)];
        }

        public async Task<IEnumerable<ProductWithMovementsResponse>?> GetProductsWithMovements()
        {
             var products = await _productRepository.GetAllProducts() ?? [];
            var allMovements = await _movementRepository.GetAllMovements() ?? [];

            // Agrupar movimientos por ProductId
            var movementsGroupedByProduct = allMovements.GroupBy(m => m.ProductId);
       
            // Mapear productos y sus movimientos
            return products
            .OrderBy(p => p.Name)
            .Select(product =>
            {
                if (product == null) return null;
                var movementsResponse = GetMovementsResponse(movementsGroupedByProduct, product);
                return movementsResponse != null 
                    ? CreateProductWithMovementsResponse(product, movementsResponse)
                    : null;
            })
            .Where(response => response != null)
            .Cast<ProductWithMovementsResponse>(); 
        }

        private IEnumerable<MovementResponse> GetMovementsResponse( IEnumerable<IGrouping<Guid?, Movement>> movementsGroupedByProduct,  Product product){
            var movements = movementsGroupedByProduct.FirstOrDefault(g => g.Key == product.Id)?.ToList() ?? [];
            return _mapper.Map<IEnumerable<MovementResponse>>(movements);
        }

        private ProductWithMovementsResponse CreateProductWithMovementsResponse(Product product, IEnumerable<MovementResponse> movementsResponse)
        {
            var purchaseAmount = _kardexCalculators.SumSales(movementsResponse,MovementForm.entrada);
            var salesAmount = _kardexCalculators.SumSales(movementsResponse,MovementForm.salida);
            var finalAmount = _kardexCalculators.FinalAmount(movementsResponse);
            var totalPurchaseBalance = _kardexCalculators.TotalPurchaseBalance(movementsResponse);
            var averageBalance = _kardexCalculators.AverageBalance(purchaseAmount, totalPurchaseBalance);
            var finalBalance = _kardexCalculators.FinalBalance(finalAmount, averageBalance);
            
            return new ProductWithMovementsResponse
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Movements = movementsResponse,
                PurchaseAmount = purchaseAmount,
                SalesAmount = salesAmount,
                FinalAmount = finalAmount,
                TotalPurchaseBalance = totalPurchaseBalance,
                AverageBalance = averageBalance,
                FinalBalance = finalBalance,

            };
        }

        public async Task<byte[]> GenerateReport()
        {
            //need to know products and get a movement response list before 
            //get something like product with movement response
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);  

            var productWithMovements = await GetProductsWithMovements();   
            if (productWithMovements == null || !productWithMovements.Any()) 
                return Array.Empty<byte>();

            
            // Escribir los encabezados
            csv.WriteField("Product Name");
            csv.WriteField("Final Amount");
            csv.WriteField("Average Balance");
            csv.WriteField("Final Balance");
            await csv.NextRecordAsync();

            foreach (var producto in productWithMovements)
            {
                csv.WriteField(producto.ProductName);
                csv.WriteField(producto.FinalAmount);
                csv.WriteField(producto.AverageBalance);
                csv.WriteField(producto.FinalBalance);
                await csv.NextRecordAsync();
            }

            // Asegurarse de que se haya escrito en el stream
            await writer.FlushAsync();

            // Retornar el contenido del archivo CSV como un array de bytes
            return memoryStream.ToArray();
        
    }
}
}