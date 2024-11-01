namespace GestionInventario.src.Modules.Movements.Domains.DTOs
{
    public class MovementDto
    {
        public required Guid Id { get; set; }
        public required string Date { get; set; }
        public required string ProductName { get; set; }
        public required int Amount { get; set; }
        public required string Reason { get; set; }
    }
}

       
        