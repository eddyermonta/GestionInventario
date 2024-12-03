using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;

namespace GestionInventario.src.Modules.Notifications.Alerts.Repositories
{
    public interface IAlertRepository
    {
        Task<IEnumerable<StockAlert>> GetAlertsByStatusAsync(bool isResolved);
        Task AddAlertAsync(StockAlert alert);
        Task<IEnumerable<StockAlert>> GetAlertsByProductIdAsync(int productId);
        Task<StockAlert> GetAlertByIdAsync(int alertId);
        Task<bool> UpdateAlertAsync(StockAlert alert);

    }
}