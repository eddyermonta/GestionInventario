
using System.ComponentModel.DataAnnotations;

namespace GestionInventario.src.Modules.Users.Domains.DTOS
{
    public class AddressUpdateRequest
    {
        [RegularExpression(@"^\d{7,}$", ErrorMessage = "El código postal debe tener al menos 7 dígitos.")]
        public int? ZipCode { get; set; }

        [StringLength(50, ErrorMessage = "La calle debe tener un máximo de 50 caracteres.")]
        public string? Street { get; set; }

        [MaxLength(50, ErrorMessage = "El estado debe tener un máximo de 50 caracteres.")]
        public string? State { get; set; }

        [MaxLength(50, ErrorMessage = "La ciudad debe tener un máximo de 50 caracteres.")]
        public string? City { get; set; }

        [MaxLength(50, ErrorMessage = "El país debe tener un máximo de 50 caracteres.")]
        public string? Country { get; set; }
    }
}