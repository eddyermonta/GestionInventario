using GestionInventario.Domain.Dto;
using GestionInventario.Domain.Enums;
using GestionInventario.Domain.Models;

namespace GestionInventario.Repository;



public class UserRepository : IUserRepository
{
    private static readonly Dictionary<string, User> usersByEmail = [];


    public UserRepository(){
        usersByEmail.Add("admin@localhost", new User
        {
            Id = Guid.NewGuid(),
            Name = "Admin User", // Debe tener entre 5 y 30 caracteres y solo letras y espacios
            LastName = "Admin", // Puede ser nulo, pero si se proporciona, debe cumplir con la validación
            DocumentType = DocumentType.CC,
            DocumentNumber = "123456789", // Debe tener entre 5 y 15 dígitos
            Address = new Adress
            {
                ZipCode = 11221, // Debe ser un código postal de 5 dígitos
                Street = "Calle 123", // Debe tener entre 5 y 200 caracteres
                State = "Cundinamarca", // Debe tener entre 2 y 100 caracteres
                City = "Bogotá", // Debe tener entre 3 y 100 caracteres
                Country = "Colombia" // Debe tener entre 3 y 100 caracteres
            },
            PhoneNumber = "1234567890", // Debe tener menos de 15 caracteres y solo dígitos
            IsActive = true,
            Email = "admin@localhost", // Debe ser un correo electrónico válido y requerido
            PasswordHash = "AQAAAAEAACcQ" // Debe tener al menos 8 caracteres, con mayúsculas, minúsculas, números y caracteres especiales
        });
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        return usersByEmail.Values.Select(CreateUserDto);
    }

    public UserDto? GetUserByEmail(string email)
    {
        if (!usersByEmail.TryGetValue(email, out var user))
            throw new Exception("El usuario no existe.");
        return CreateUserDto(user);
    }

    public void AddUser(UserDto userDto)
    {
        if (usersByEmail.ContainsKey(userDto.Email))
            throw new InvalidOperationException("A user with this email already exists.");
        var user = CreateAUser(userDto);
        usersByEmail.Add(user.Email,user);
    }

    public void UpdateUser(UserDto userDto, string email)
    {
        if (!usersByEmail.TryGetValue(email, out User? value))
            throw new KeyNotFoundException("El usuario no existe.");

        var user = value;

        user.Name = userDto.Name;
        user.LastName = userDto.LastName;
        user.DocumentType = userDto.DocumentType;
        user.DocumentNumber = userDto.DocumentNumber;
        if (userDto.Address != null)
        {
            user.Address.ZipCode = userDto.Address.ZipCode;
            user.Address.City = userDto.Address.City;
            user.Address.Country = userDto.Address.Country;
            user.Address.State = userDto.Address.State;
            user.Address.Street = userDto.Address.Street;
        }
        user.IsActive = userDto.IsActive; //aqui activa o inactiva el usuario
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;

        usersByEmail[email] = user;
    }

    public static User CreateAUser(UserDto userDto){
        var user = new User
        {
            Id= Guid.NewGuid(),
            Name = userDto.Name,
            LastName = userDto.LastName,
            DocumentType = userDto.DocumentType,
            DocumentNumber = userDto.DocumentNumber,
            Address = new Adress
            {
                ZipCode = userDto.Address?.ZipCode ?? 0,
                City = userDto.Address?.City ?? string.Empty,
                Country = userDto.Address?.Country ?? string.Empty,
                State = userDto.Address?.State ?? string.Empty,
                Street = userDto.Address?.Street ?? string.Empty
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

    public AuthResponse CreateResponse(bool isSuccessful, string message, Guid jwt){
        return new AuthResponse{
            IsSuccessful = isSuccessful,
            Message = message,
            Jwt = jwt
        };
    }

    public AuthResponse ValidateUser(string email, string password)
    {
        if (!usersByEmail.TryGetValue(email, out var user))
            return CreateResponse(false, "El usuario no existe.", Guid.Empty);

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return CreateResponse(false, "Contraseña incorrecta.", Guid.Empty);

        return CreateResponse(true, "Usuario autenticado.", Guid.NewGuid());
    }
}



