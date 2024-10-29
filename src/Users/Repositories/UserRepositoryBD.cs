using GestionInventario.src.Bdd;
using GestionInventario.src.Users.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Users.Repositories
{
    public class UserRepositoryBD(MyDbContext context) : IUserRepository
    {
        private readonly MyDbContext _context = context;

        public void AddUser(User user)
        {
            _context.UsersBD.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return [.. _context.UsersBD.Include(u => u.Address)];
             
        }

        public User? GetUserByEmail(string email)
        {
            return _context.UsersBD
            .Include(u => u.Address)
            .FirstOrDefault(u => u.Email == email);
        }

        public void UpdateUser(User user)
        {
            _context.UsersBD.Update(user);
            _context.SaveChanges(); 
        }
    }
}