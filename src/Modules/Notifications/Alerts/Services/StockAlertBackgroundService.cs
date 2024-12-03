
namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    
    /// <summary>
    /// Servicio en segundo plano que verifica el stock de los productos y envía alertas por correo electrónico si el stock es bajo.
    /// Este servicio se ejecuta automáticamente cada hora.
    /// </summary>
    public class StockAlertBackgroundService (IServiceProvider  serviceProvider) : BackgroundService
    {
        private readonly IServiceProvider  _serviceprovider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             while (!stoppingToken.IsCancellationRequested)
             {
                using var scope = _serviceprovider.CreateScope();
                try
                {
                    var stockAlertService = scope.ServiceProvider.GetRequiredService<IStockAlertService>();
                    await stockAlertService.CheckAndNotifyLowStockAsync();

                }catch (Exception ex)
                {
                    // Manejar errores y lanzar excepciones
                    throw new InvalidOperationException("Error al enviar el correo electrónico.", ex);
                }
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); //change this for presentation

            }
            
        }
    }
}