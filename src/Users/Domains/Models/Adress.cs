namespace GestionInventario.src.Users.Domains.Models;

public class Adress
{
    public required int ZipCode { get; set; }
    public required string Street { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}
