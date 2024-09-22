namespace GestionInventario.Repository;

using GestionInventario.Models;

public class UserRepository : IUserRepository
{
    private static readonly Dictionary<string, User> usersByEmail = [];


    public UserRepository(){
        usersByEmail.Add(
            "admin@localhost", new User
                                        {
                                        Id = Guid.NewGuid(),
                                        Name = "Admin",
                                        LastName = "Admin",
                                        DocumentType = DocumentType.CC,
                                        DocumentNumber = "123456789",
                                            Adress = new Adress{
                                                ZipCode = 1121,
                                                City = "Bogotá",
                                                Country = "Colombia",
                                                State = "Cundinamarca",
                                                Street = "Calle 123"
                                            },
                                        PhoneNumber = "1234567890",
                                        IsActive = true,
                                        PasswordHash = "AQAAAAEAACcQAAAAEJ9Q"
                                        }
                    );
    }

    public void AddUser(User user)
    {
        usersByEmail.Add("",user);
    }

    public void UpdateUser(User user, string email)
    {
        if (usersByEmail.ContainsKey(email))
        {
            usersByEmail[email] = user;
        }else
        {
            throw new KeyNotFoundException("El usuario no existe.");
        }
    }

    public IEnumerable<User> GetAllUsers()
    {
        return usersByEmail.Values;
    }

    public User? GetUserByEmail(string email)
    {
        if (usersByEmail.TryGetValue(email, out var user))
        {
            return user;
        }
        return null; // O puedes lanzar una excepción si prefieres
    }

    public void ActivateUser(string email)
    {
        if (usersByEmail.TryGetValue(email, out var user))
        {
            user.IsActive = true;
        }
        else
        {
            throw new KeyNotFoundException("El usuario no existe.");
        }
    }

    public void DeactivateUser(string email)
    {
          if (usersByEmail.TryGetValue(email, out var user))
        {
            user.IsActive = false;
        }
        else
        {
            throw new KeyNotFoundException("El usuario no existe.");
        }
    }

}



