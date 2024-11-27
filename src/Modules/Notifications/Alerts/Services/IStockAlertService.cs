
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;

namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public interface IStockAlertService
    {
        Task<IEnumerable<StockAlert>> GetLowStockProductsAsync();
        Task CheckAndNotifyLowStockAsync();
        Task<bool> ResolveAlertAsync(int alertId);
        Task<IEnumerable<StockAlert>> GetActiveAlertsAsync();

    }
}