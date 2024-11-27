using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;

namespace GestionInventario.src.Modules.Notifications.Alerts.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        public Task AddAlertAsync(StockAlert alert)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StockAlert>> GetAlertsByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StockAlert>> GetAllAlertsAsync()
        {
            throw new NotImplementedException();
        }
    }
}