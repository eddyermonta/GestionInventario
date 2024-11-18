using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Services
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