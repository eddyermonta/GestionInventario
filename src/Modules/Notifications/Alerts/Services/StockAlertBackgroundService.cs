
namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public class StockAlertBackgroundService (IStockAlertService stockAlertService) : BackgroundService
    {
        private readonly IStockAlertService _stockAlertService = stockAlertService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _stockAlertService.CheckAndNotifyLowStockAsync();
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error: {ex.Message}");
            }
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
        }
    }
}