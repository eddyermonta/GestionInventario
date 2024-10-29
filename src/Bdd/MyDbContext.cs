using GestionInventario.src.Users.Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Bdd
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<User> UsersBD { get; set; } 
        public DbSet<Address> AddressesBD { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseNpgsql();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
            .HasOne(u => u.Address) // Un User tiene un Address
            .WithOne() // Un Address está asociado a un solo User
            .HasForeignKey<Address>(a => a.UserId); // Configura la clave foránea en Address // Configurar la clave foránea
        
        }
    }
}