
using GestionInventario.src.Modules.Movements.Domains.DTOs;

namespace GestionInventario.src.Modules.Inventories.Domains.Models
{
    public class Inventary
    {
        public Dictionary<Guid, MovementRequest> Movements { get; set; } = [];
    }
}