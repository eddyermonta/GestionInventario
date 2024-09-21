namespace GestionInventario.Repository;

using GestionInventario.Models;

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = [];

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
            user.Isactive = true;
        }
    }

    public void DeactivateUser(Guid id)
    {
        var user = users.FirstOrDefault(c => c.Id == id);
        if (user != null)
        {
            user.Isactive = false;
        }
    }
}



