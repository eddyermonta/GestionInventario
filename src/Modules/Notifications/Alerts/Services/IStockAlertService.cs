
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Dtos;
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;

namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public interface IStockAlertService
    {
        Task<IEnumerable<StockAlertResponse>?> GetLowStockProductsAsync();
        Task CheckAndNotifyLowStockAsync();
        Task<bool> ResolveAlertAsync(Guid alertId);
        Task<IEnumerable<StockAlertResponse>> GetAlertsByStatusAsync();

    }
}