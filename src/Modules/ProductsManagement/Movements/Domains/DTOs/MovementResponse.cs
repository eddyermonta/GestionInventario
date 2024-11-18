using System.Text.Json.Serialization;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs
{
    public class MovementResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MovementType Type { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MovementForm CategoryMov { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Reason { get; set; }
        public Guid? ProductId { get; set; }

    }
}

