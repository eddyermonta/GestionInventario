namespace GestionInventario.src.Modules.Users.Domains.DTOS
{
    public class UserResponse
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public AddressResponse? Address { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
    }
}