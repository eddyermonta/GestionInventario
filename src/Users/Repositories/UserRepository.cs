
using GestionInventario.src.Shared;
using GestionInventario.src.Users.Domains.DTOs;
using GestionInventario.src.Users.Domains.Models;


namespace GestionInventario.src.Users.Repositories;
public class UserRepository(IAuthUserCache authUserCache): IUserRepository
{
    private readonly Dictionary<string, User> usersByEmail = authUserCache.GetUsersByEmail();
    
    public IEnumerable<UserDto> GetAllUsers()
    {
        return usersByEmail.Values.Select(CreateUserDto);
    }

    public UserDto? GetUserByEmail(string email)
    {
        if (!usersByEmail.TryGetValue(email, out var user))
            throw new KeyNotFoundException("El usuario no existe.");
        return CreateUserDto(user);
    }

    public void AddUser(UserDto userDto)
    {
        if (usersByEmail.ContainsKey(userDto.Email))
            throw new InvalidOperationException("A user with this email already exists.");
        var user = CreateAUser(userDto);
        usersByEmail.Add(user.Email,user);
    }

    public void UpdateUser(UserDto user, string email)
    {
        if (!usersByEmail.TryGetValue(email, out User? value))
            throw new KeyNotFoundException("El usuario no existe.");

        var existingUser = value;

        existingUser.Name = user.Name;
        existingUser.LastName = user.LastName;
        existingUser.DocumentType = user.DocumentType;
        existingUser.DocumentNumber = user.DocumentNumber;
        if (existingUser.Address != null && user.Address != null)
        {
            existingUser.Address.ZipCode = user.Address.ZipCode;
            existingUser.Address.City = user.Address.City;
            existingUser.Address.Country = user.Address.Country;
            existingUser.Address.State = user.Address.State;
            existingUser.Address.Street = user.Address.Street;
        }
        existingUser.IsActive = user.IsActive; //aqui activa o inactiva el usuario
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Email = user.Email;

        usersByEmail[email] = existingUser;
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
}



