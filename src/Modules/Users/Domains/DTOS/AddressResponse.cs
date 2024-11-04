namespace GestionInventario.src.Modules.Users.Domains.DTOS
{
    public class AddressResponse
    {
        public int ZipCode { get; set; }
        public string? Street { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}