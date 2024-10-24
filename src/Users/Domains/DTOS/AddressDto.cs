using System.ComponentModel.DataAnnotations;

namespace GestionInventario.src.Users.Domains.DTOs;

public class AddressDto {
    [RegularExpression(@"^\d{5}$")]
    [DataType(DataType.PostalCode)]
    [Display(Name = "Zip Code")]
    public required int ZipCode { get; set; }
    [StringLength(200)]
    [DataType(DataType.Text)]
    [Display(Name = "Street")]
    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    public required string Street { get; set; }
    [StringLength(100)]
    [DataType(DataType.Text)]
    [Display(Name = "State")]
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public required string State { get; set; }
    [StringLength(100)]
    [DataType(DataType.Text)]
    [Display(Name = "City")]
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public required string City { get; set; }
    [StringLength(100)]
    [DataType(DataType.Text)]
    [Display(Name = "Country")]
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public required string Country { get; set; }
}