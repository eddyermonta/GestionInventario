
using GestionInventario.src.Bdd;
using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.Auths.Repositories
{
    public class AuthRepositoryBD(MyDbContext context) : IAuthRepository
    {
        private readonly MyDbContext _context = context;
        
        public User? GetUserByEmail(string email)
        {
            // Fetch user from the database based on email
            return _context.UsersBD.FirstOrDefault(u => u.Email == email);

        }
    }
}