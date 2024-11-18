
using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;

namespace GestionInventario.src.Core.Shared
{
    public class AuthUserCache : IAuthUserCache
    {
        private static readonly Dictionary<string, User> usersByEmail = [];


    public AuthUserCache()
    {
        usersByEmail.Add("admin@localhost", new User
        {
            DocumentNumber = "123456789", // Debe tener entre 5 y 15 dígitos
            Address = new Address
            {
                ZipCode = 11221, // Debe ser un código postal de 5 dígitos
                Street = "Calle 123", // Debe tener entre 5 y 200 caracteres
                State = "Cundinamarca", // Debe tener entre 2 y 100 caracteres
                City = "Bogotá", // Debe tener entre 3 y 100 caracteres
                Country = "Colombia", // Debe tener entre 3 y 100 caracteres
                Supplier = default!, // Set the required Supplier member
                User = default! // Set the required User member
            },
            PhoneNumber = "1234567890", // Debe tener menos de 15 caracteres y solo dígitos
            IsActive = true,
            Email = "admin@localhost", // Debe ser un correo electrónico válido y requerido
            PasswordHash = "AQAAAAEAACcQ" // Debe tener al menos 8 caracteres, con mayúsculas, minúsculas, números y caracteres especiales
        });
    }
        public Dictionary<string, User> GetUsersByEmail()
        {
            return usersByEmail;
        }
    }
}