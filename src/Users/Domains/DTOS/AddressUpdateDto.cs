
using System.ComponentModel.DataAnnotations;

namespace GestionInventario.src.Users.Domains.DTOS
{
    public class AddressUpdateDto
    {
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe tener 5 dígitos.")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code")]
        public int? ZipCode { get; set; }

        [StringLength(200, ErrorMessage = "La calle debe tener entre 5 y 200 caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "Street")]
        [MinLength(5, ErrorMessage = "La calle debe tener al menos 5 caracteres.")]
        [MaxLength(200, ErrorMessage = "La calle debe tener un máximo de 200 caracteres.")]
        public string? Street { get; set; }

        [StringLength(100, ErrorMessage = "El estado debe tener entre 2 y 100 caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        [MinLength(2, ErrorMessage = "El estado debe tener al menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "El estado debe tener un máximo de 100 caracteres.")]
        public string? State { get; set; }

        [StringLength(100, ErrorMessage = "La ciudad debe tener entre 3 y 100 caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        [MinLength(3, ErrorMessage = "La ciudad debe tener al menos 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "La ciudad debe tener un máximo de 100 caracteres.")]
        public string? City { get; set; }

        [StringLength(100, ErrorMessage = "El país debe tener entre 3 y 100 caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        [MinLength(3, ErrorMessage = "El país debe tener al menos 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "El país debe tener un máximo de 100 caracteres.")]
        public string? Country { get; set; }
    }
}