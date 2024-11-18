
namespace GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models
{
    public class Mesurement
    {
        public int Value { get; set; }
        public UnitMeasurement Unit { get; set; }

         public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
    }

   