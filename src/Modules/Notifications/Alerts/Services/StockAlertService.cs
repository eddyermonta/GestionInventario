using GestionInventario.src.Modules.Notifications.Alerts.Domain.Dtos;
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;
using GestionInventario.src.Modules.Notifications.Alerts.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Movements.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Products.Repositories;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public class StockAlertService (
        IEmailService emailService, IMovementRepository movementRepository, IAlertRepository alertRepository, IProductRepository productRepository, IOptions<EmailSettings> emailSettings
        ) : IStockAlertService
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IMovementRepository _movementRepository = movementRepository;
        private readonly IAlertRepository _alertRepository = alertRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly EmailSettings _emailSettings = emailSettings.Value;


        public async Task CheckAndNotifyLowStockAsync()
        {
            var lowStockProducts = await GetLowStockProductsAsync();

            /*foreach (var alert in lowStockProducts)
            {
                // Notificar por correo
                var subject = $"Alerta de stock bajo para el producto {alert.ProductId}";
                var body = $"El producto {alert.ProductId} tiene un stock actual de {alert.CurrentStock}, " +
                        $"por debajo del mínimo permitido de .";
                await _emailService.SendEmailAsync("admin@company.com", subject, body);
            }*/
            if(_emailSettings.Email == null)
                Console.WriteLine("emain nulo");
            else await _emailService.SendEmailAsync(_emailSettings.Email, "test", "esta es una prueba");
            

            
        }

        public Task<IEnumerable<StockAlert>> GetActiveAlertsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StockAlert>> GetLowStockProductsAsync()
        {
            var movements = await _movementRepository.GetAllMovements();
        

            // Agrupar movimientos por producto y calcular cantidad total
            var stockLevels = movements
                .GroupBy(m => m.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    CurrentStock = 10,//g.Sum(m => m.MovementType == MovementType.Entrada ? m.Quantity : -m.Quantity),
                    MinimumStock = 3 // Si cada movimiento tiene esta info
                });

            // Filtrar productos con stock por debajo del mínimo
            return stockLevels
                .Where(s => s.CurrentStock < s.MinimumStock)
                .Select(s => new StockAlert
                {
                    ProductId = 1,
                    CurrentStock = s.CurrentStock,
                    MinimumStock = s.MinimumStock,
                    AlertDate = DateTime.UtcNow
                })
                .ToList();
                
        }

        public async Task<bool> ResolveAlertAsync(int alertId)
        {
             // Obtener alerta
            await _alertRepository.GetAlertsByProductIdAsync(alertId);
            return true;
            //var alert = await _alertRepository.GetAllAlertsAsync(alertId) ?? throw new Exception("Alert not found");

            /*
            // Consultar producto asociado
            var product = await _productRepository.GetByIdAsync(alert.ProductId) ?? throw new Exception("Product not found");

            // Calcular cantidad a pedir
            var stockActual = await _movementRepository.GetStockByProductIdAsync(product.Id);
            var cantidadAPedir = product.MaxStock - stockActual;

            if (cantidadAPedir <= 0)
                throw new Exception("No es necesario realizar un pedido. El stock está dentro del rango permitido.");

            // Crear pedido
            var pedido = new Pedido
            {
                ProductId = product.Id,
                Quantity = cantidadAPedir,
                SupplierId = product.SupplierId,
                Status = PedidoStatus.Pendiente,
                CreatedAt = DateTime.UtcNow
            };
            await _pedidoRepository.AddAsync(pedido);

            // Actualizar alerta como resuelta
            alert.Status = AlertStatus.Resuelta;
            alert.ResolvedAt = DateTime.UtcNow;
            await _alertRepository.UpdateAsync(alert);

            // Notificar por correo
            var emailContent = $"Se ha generado un pedido automático para el producto {product.Name}. " +
                            $"Cantidad: {cantidadAPedir}. Proveedor: {product.SupplierName}.";
            await _emailService.SendEmailAsync("admin@tuempresa.com", "Pedido Automático Generado", emailContent);
            */
        }
    }
}