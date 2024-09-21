namespace GestionInventario.Repository;

using GestionInventario.Models;

public class UserRepository : IUserRepository
{

        private static readonly List<User> users =
        [
            new() {
                Id = Guid.NewGuid(),
                Name = "John",
                LastName = "Doe",
                DocumentType = DocumentType.CC,
                DocumentNumber = "123456789",
                Adress = new Adress {
                    ZipCode = "12345",
                    Street = "123 Main St",
                    State = "New York",
                    City = "New York",
                    Country = "USA"
                    },
                PhoneNumber = "555-1234",
                IsActive = true,
                Email = "john.doe@example.com",
                PasswordHash = "password123"
                },

                new() {
                Id = Guid.NewGuid(),
                Name = "Jane",
                LastName = "Smith",
                DocumentType = DocumentType.CE,
                DocumentNumber = "987654321",
                Adress = new Adress {
                    ZipCode = "67890",
                    Street = "456 Elm St",
                    State = "California",
                    City = "Los Angeles",
                    Country = "USA"
                    },
                PhoneNumber = "555-5678",
                IsActive = true,
                Email = "jane.smith@example.com",
                PasswordHash = "password456"
                }
        ];

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void UpdateUser(User user)
    {
        var index = users.FindIndex(c=> c.Id == user.Id);
        if (index > -1)
        {
            users[index] = user;
        }
    }

    public IEnumerable<User> GetAllUsers()
    {
        return users.AsReadOnly();
    }

    public User? GetUserByEmail(string email)
    {
        return users.FirstOrDefault(c => c.Email.Equals(email));
    }

    public void ActivateUser(Guid id)
    {
        var user = users.FirstOrDefault(c => c.Id == id);
        if (user != null)
        {
            user.IsActive = true;
        }
    }

    public void DeactivateUser(Guid id)
    {
        var user = users.FirstOrDefault(c => c.Id == id);
        if (user != null)
        {
            user.IsActive = false;
        }
    }
}



