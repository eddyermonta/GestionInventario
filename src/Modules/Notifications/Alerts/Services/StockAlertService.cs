using AutoMapper;
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Dtos;
using GestionInventario.src.Modules.Notifications.Alerts.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Movements.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Movements.Services;
using GestionInventario.src.Modules.ProductsManagement.Products.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs;
using Microsoft.Extensions.Options;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;

namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public class StockAlertService (
        IEmailService emailService,
        IAlertRepository alertRepository,
        IOptions<EmailSettings> emailSettings,
        IMovementRepository movementRepository,
        IKardexCalculators kardexCalculators,
        IProductRepository productRepository,
        IMovementManualService movementManualService,
        IMapper mapper
        ) : IStockAlertService
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IAlertRepository _alertRepository = alertRepository;
        private readonly IKardexCalculators _kardexCalculators = kardexCalculators;
        private readonly EmailSettings _emailSettings = emailSettings.Value;
        private readonly IMovementRepository _movementRepository = movementRepository;
        private readonly IMovementManualService _movementManualService = movementManualService;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task CheckAndNotifyLowStockAsync()
        {
            var lowStockProducts = await GetLowStockProductsAsync();
            if (lowStockProducts == null || !lowStockProducts.Any())
            {
                return;
            }
            
            // Para evitar conflictos de concurrencia con el DbContext, procesamos de forma secuencial
            foreach (var alert in lowStockProducts)
            {
                // Verificar si la alerta no ha sido resuelta
                if (!alert.IsResolved)
                {
                    // Obtener el producto completo de manera secuencial para evitar el uso concurrente del DbContext
                    var product = await _productRepository.GetProductById(alert.ProductId);

                    // Asegurarse de que el producto exista
                    if (product != null)
                    {
                        var subject = $"Alerta de stock bajo para el producto {product.Name}";
                        var body = $"El producto {product.Name} tiene un stock actual de {alert.CurrentStock}, " +
                                $"por debajo del mínimo permitido de {alert.MinimumStock}. " +
                                $"Por favor, tome las acciones necesarias.";

                        // Enviar correo con la información de la alerta
                        await _emailService.SendEmailAsync(_emailSettings.Email ?? "eduardo.montano@utp.edu.co", subject, body);
                    }
                }
            }
        }


        public async Task<IEnumerable<StockAlertResponse>> GetAlertsByStatusAsync()
        {
             // Obtener todas las alertas que no han sido resueltas (IsResolved == false)
            var activeAlerts = await _alertRepository.GetAlertsByStatusAsync(false);

            // Mapear las alertas a StockAlertResponse (si es necesario)
            var activeAlertResponses = _mapper.Map<IEnumerable<StockAlertResponse>>(activeAlerts);

            return activeAlertResponses;
        }

        public async Task<IEnumerable<StockAlertResponse>?> GetLowStockProductsAsync()
        {
            var movements = await _movementRepository.GetAllMovements();

            // Calcular productos de bajo stock
            var stockLevels = new List<StockAlertResponse>();

            foreach (var g in movements.GroupBy(m => m.ProductId))
            {
                var product = g.Key.HasValue ? await _productRepository.GetProductById(g.Key.Value) : null;

                if (product == null) continue;

                var finalAmount = _kardexCalculators.FinalAmount(_mapper.Map<IEnumerable<MovementResponse>>(g));

                if (finalAmount < product.MinimumStock)
                {
                     var existingAlert = await _alertRepository.GetAlertsByProductIdAsync(product.Id);

                    if (existingAlert != null && existingAlert.Any(alert => !alert.IsResolved))
                    {
                        continue; // La alerta ya está en la base de datos y no se resuelve, así que no la insertamos de nuevo.
                    }


                   var stockAlertResponse = new StockAlertResponse
                    {
                        ProductId = g.Key.HasValue ? g.Key.Value : Guid.Empty,
                        AlertDate = DateTime.UtcNow,
                        IsResolved = false,
                        IsConfirmed = false,
                        ResolvedDate = null,
                        CurrentStock = finalAmount,
                        MinimumStock = product.MinimumStock,
                    };
                    
                    var stockAlertEntity = _mapper.Map<StockAlert>(stockAlertResponse);
                     await _alertRepository.AddAlertAsync(stockAlertEntity);
                      stockLevels.Add(stockAlertResponse);
                }
            }

            return stockLevels;
        }


        public async Task<bool> ResolveAlertAsync(Guid alertId)
        {
            // Paso 1: Obtener la alerta por su ID
            var alert = await _alertRepository.GetAlertByIdAsync(alertId) ?? throw new KeyNotFoundException($"No se encontró una alerta con el ID {alertId}.");

            // Paso 2: Verificar si la alerta ya fue resuelta
            if (alert.IsResolved)  return false;

            // Paso 3: Obtener el producto asociado a la alerta
            var product = await _productRepository.GetProductById(alert.ProductId) ?? throw new KeyNotFoundException($"No se encontró un producto con el ID {alert.ProductId}.");

            // Paso 4: Obtener los movimientos y calcular las métricas con el método existente
            var productWithMovements = _movementManualService.GetMovementsByProductId(product);
            var movements = await productWithMovements ?? [];
            var finalAmount = movements.FirstOrDefault()?.FinalAmount ?? 0;

            // Paso 5: Calcular la cantidad necesaria para reabastecer
            var stockToOrder = product.MaximumStock - finalAmount;
            if (stockToOrder <= 0)
            {
                throw new InvalidOperationException($"El stock actual ya está en el máximo permitido o por encima.");
            }

            // Usar el método existente para agregar el movimiento
           await _movementRepository.AddMovement
           (new() {
                        Id = Guid.NewGuid(),
                        Date = DateTime.UtcNow,
                        Type = MovementType.SupplierReceipt,
                        CategoryMov = MovementForm.entrada,
                        Amount = stockToOrder,
                        UnitPrice = product.UnitPrice,
                        Reason = "Compra a provedor para reabastecimiento",
                        ProductId = product.Id,
                        Product = product
                    });
                        


            // Paso 7: Actualizar la alerta como resuelta
            alert.IsResolved = true;
            alert.ResolvedDate = DateTime.UtcNow;
            await _alertRepository.UpdateAlertAsync(alert);

            // Paso 8: Retornar éxito
            return true;
        }

    }
}