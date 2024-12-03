using GestionInventario.src.Data;
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.Notifications.Alerts.Repositories
{
    public class AlertRepository ( MyDbContext myDbContext)  : IAlertRepository
    {
        private readonly MyDbContext _context = myDbContext;
        public async Task AddAlertAsync(StockAlert alert)
        {
            await _context.StockAlertsBD.AddAsync(alert);
            await _context.SaveChangesAsync();
        }

        public async Task<StockAlert> GetAlertByIdAsync(int alertId)
        {
            var alert = await _context.StockAlertsBD.FindAsync(alertId);
            return alert ?? throw new InvalidOperationException("Alert not found");
        }

        public async Task<IEnumerable<StockAlert>> GetAlertsByProductIdAsync(int productId)
        {
            return await _context.StockAlertsBD.ToListAsync();
        }

        public async Task<IEnumerable<StockAlert>> GetAlertsByStatusAsync(bool isResolved)
        {
            return await _context.StockAlertsBD
                                .Where(alert => alert.IsResolved == isResolved)
                                .ToListAsync();
        }

        public async Task<bool> UpdateAlertAsync(StockAlert alert)
        {
             _context.StockAlertsBD.Update(alert);
                return await _context.SaveChangesAsync() > 0;
        }
    }
}