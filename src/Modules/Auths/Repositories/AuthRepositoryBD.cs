
using GestionInventario.src.Data;
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Auths.Repositories
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