
using GestionInventario.src.Modules.Movements.Domains.DTOs;

namespace GestionInventario.src.Modules.Inventories.Domains.Models
{
    public class Inventary
    {
        public Dictionary<Guid, MovementDto> Movements { get; set; } = [];
    }
}