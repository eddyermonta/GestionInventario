



using GestionInventario.src.Core.Shared;
using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Users.Repositories;
public class UserRepository(IAuthUserCache authUserCache): IUserRepository
{
    private readonly Dictionary<string, User> usersByEmail = authUserCache.GetUsersByEmail();
    
    public IEnumerable<User> GetAllUsers()
    {
        return usersByEmail.Values;
    }

    public User? GetUserByEmail(string email)
    {
        if (!usersByEmail.TryGetValue(email, out var user))
                throw new KeyNotFoundException("El usuario no existe.");
            return user;
    }

    public void AddUser(User user)
    {
        if (usersByEmail.ContainsKey(user.Email))
                throw new InvalidOperationException("Ya existe un usuario con este correo electrónico.");
            
            // Asigna un nuevo ID y hashea la contraseña aquí si es necesario
            user.Id = Guid.NewGuid();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // Asegúrate de que PasswordHash se maneje correctamente.

            usersByEmail.Add(user.Email, user);
    }

   public void UpdateUser(User user)
{
    // Verifica si el usuario existe en el diccionario
    if (!usersByEmail.TryGetValue(user.Email, out var existingUser))
        throw new KeyNotFoundException("El usuario no existe.");

    // Actualiza las propiedades del usuario existente con los datos del nuevo objeto user
    existingUser.Name = user.Name;
    existingUser.LastName = user.LastName;
    existingUser.DocumentType = user.DocumentType;
    existingUser.DocumentNumber = user.DocumentNumber;

    // Actualiza la dirección si existe
    if (existingUser.Address != null && user.Address != null)
    {
        existingUser.Address.ZipCode = user.Address.ZipCode;
        existingUser.Address.City = user.Address.City;
        existingUser.Address.Country = user.Address.Country;
        existingUser.Address.State = user.Address.State;
        existingUser.Address.Street = user.Address.Street;
    }

    existingUser.IsActive = user.IsActive; // Activa o inactiva el usuario
    existingUser.PhoneNumber = user.PhoneNumber;

    // No es necesario reasignar existingUser a usersByEmail, ya que ya está referenciado
}

    public static User CreateAUser(UserDto userDto){
        var user = new User
        {
            Id= Guid.NewGuid(),
            Name = userDto.Name,
            LastName = userDto.LastName,
            DocumentType = userDto.DocumentType,
            DocumentNumber = userDto.DocumentNumber,
            Address = new Address
            {
                ZipCode = userDto.Address?.ZipCode ?? 0,
                City = userDto.Address?.City ?? string.Empty,
                Country = userDto.Address?.Country ?? string.Empty,
                State = userDto.Address?.State ?? string.Empty,
                Street = userDto.Address?.Street ?? string.Empty,
                Supplier = default!,
                User = default!
            },
            PhoneNumber = userDto.PhoneNumber,
            IsActive = userDto.IsActive,
            Email = userDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
        };
        return user;
    }

    public static UserDto CreateUserDto(User user){
            return new UserDto
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    DocumentType = user.DocumentType,
                    DocumentNumber = user.DocumentNumber,
                    Address = new AddressDto
                    {
                        ZipCode = user.Address.ZipCode,
                        City = user.Address.City,
                        Country = user.Address.Country,
                        State = user.Address.State,
                        Street = user.Address.Street
                    },
                    IsActive = user.IsActive,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
        }
}



