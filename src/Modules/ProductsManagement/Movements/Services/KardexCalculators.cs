using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Services
{
    public class KardexCalculators : IKardexCalculators
    {
        public decimal AverageBalance(decimal amountPurchase, decimal totalPurchase)
        {
             return totalPurchase / amountPurchase;
        }

        public int FinalAmount(IEnumerable<MovementResponse> movements)
        {
            var entries = SumSales(movements, MovementForm.entrada);
            var outs = SumSales(movements, MovementForm.salida);
            return entries - outs;
        }

        public decimal TotalPurchaseBalance (IEnumerable<MovementResponse> movements)
        {
            return movements.Where(m => m.CategoryMov == MovementForm.entrada).Sum(m => m.Amount * m.UnitPrice);
        }

        public decimal FinalBalance(int totalAmounts, decimal averageBalance)
        {
            return totalAmounts * averageBalance;
        }

        public int SumSales(IEnumerable<MovementResponse> movements, MovementForm movementForm)
        {
            return movements.Where(m => m.CategoryMov == movementForm).Sum(m => m.Amount);
        }
    }
}

