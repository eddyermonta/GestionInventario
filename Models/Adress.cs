namespace GestionInventario.Models;

using System.ComponentModel.DataAnnotations;

public class Adress
{
    [StringLength(10)]
    public required string ZipCode { get; set; }
    [StringLength(200)]
    public required string Street { get; set; }
    [StringLength(100)]
    public required string State { get; set; }
    [StringLength(100)]
    public required string City { get; set; }
    [StringLength(100)]
    public required string Country { get; set; }
}
