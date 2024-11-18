using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Services
{
    public interface IKardexCalculators
    {
        decimal AverageBalance(decimal amountPurchase, decimal totalPurchase);
        int FinalAmount(IEnumerable<MovementResponse> movements);
        decimal TotalPurchaseBalance (IEnumerable<MovementResponse> movements);
        decimal FinalBalance(int totalAmounts, decimal averageBalance);
        int SumSales(IEnumerable<MovementResponse> movements, MovementForm movementForm);


         

    }
}