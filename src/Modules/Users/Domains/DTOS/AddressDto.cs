using System.ComponentModel.DataAnnotations;

namespace GestionInventario.src.Modules.Users.Domains.DTOs;

public class AddressRequest {

    [RegularExpression(@"^\d{7,}$")]
    public required int ZipCode { get; set; }
    [StringLength(200)]
    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    public required string Street { get; set; }
    [StringLength(100)]
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public required string State { get; set; }
    [StringLength(100)]
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public required string City { get; set; }
    [StringLength(100)]

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public required string Country { get; set; }
}