using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Domains.Models.Enum;

namespace GestionInventario.src.Modules.Movements.Services
{
    public interface IKardexCalculators
    {
        int TotalAmounts(IEnumerable<MovementResponse> movements, MovementForm movementForm);
        int CalculateTotalAmounts(IEnumerable<MovementResponse> movements);
        decimal CalculateTotalPurchase(IEnumerable<MovementResponse> movements);
        decimal AverageBalance(decimal amountPurchase, decimal totalPurchase);
        decimal FinalBalance(int totalAmounts, decimal averageBalance);
    }
}