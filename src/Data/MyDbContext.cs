using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Suppliers.Domains.Models;
using GestionInventario.src.Modules.Users.Domains.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Data
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public required DbSet<User> UsersBD { get; set; } 
        public required DbSet<Address> AddressesBD { get; set; }
        public required DbSet<Product> ProductsBD { get; set; }
        public required DbSet<Supplier> SuppliersBD { get; set; }
        public required DbSet<Category> CategoriesBD { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseNpgsql();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relación muchos a muchos entre Productos y Categorías
            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            // Relación uno a muchos entre Productos y Proveedores
            builder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade); // Establecer SupplierId en NULL si el proveedor es eliminado

            // Relación uno a uno entre Usuarios y Direcciones
            builder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<User>(u => u.AddressId) // Asegúrate de que esta clave foránea es correcta
                .OnDelete(DeleteBehavior.Cascade); // Eliminar Address si User es eliminado

            // Relación uno a uno entre Proveedores y Direcciones
            builder.Entity<Supplier>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Supplier)
                .HasForeignKey<Supplier>(s => s.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
