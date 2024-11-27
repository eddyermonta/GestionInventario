using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;

namespace GestionInventario.src.Modules.Notifications.Alerts.Repositories
{
    public interface IAlertRepository
    {
        Task<IEnumerable<StockAlert>> GetAllAlertsAsync();
        Task AddAlertAsync(StockAlert alert);
        Task<IEnumerable<StockAlert>> GetAlertsByProductIdAsync(int productId);

    }
}