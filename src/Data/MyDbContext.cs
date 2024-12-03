

using GestionInventario.src.Modules.Notifications.Alerts.Domain.Models;
using GestionInventario.src.Modules.ProductsManagement.Categories.Domain.Models;
using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models;
using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.Model;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Data
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : IdentityDbContext<User>(options)
    {
        public required DbSet<Address> AddressesBD { get; set; }
        public required DbSet<Product> ProductsBD { get; set; }
        public required DbSet<Supplier> SuppliersBD { get; set; }
        public required DbSet<Category> CategoriesBD { get; set; }
        public required DbSet<ProductCategory> ProductCategoriesBD { get; set; }
        public required DbSet<Movement> MovementsBD { get; set; }
        public required DbSet<StockAlert> StockAlertsBD { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relación muchos a uno entre Alerts y Product
            builder.Entity<StockAlert>()
                .HasOne(a => a.Product) // Cada alerta está asociada a un producto
                .WithMany(p => p.StockAlerts) // Un producto puede tener varias alertas
                .HasForeignKey(a => a.ProductId) // Clave foránea en Alert
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminar productos si tienen alertas asociadas


            // Relación muchos a muchos entre Productos y Categorías
            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a muchos entre Productos y Proveedores
            builder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict); // No permitir eliminar un proveedor si tiene productos asociados

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

            builder.Entity<Product>()
            .OwnsOne(p => p.Weight, w =>
            {
                w.Property(x => x.Value).HasColumnName("Weight_Value");
                w.Property(x => x.Unit).HasColumnName("Weight_Unit");
            });

            builder.Entity<Product>()
            .HasMany(p => p.Movements)
            .WithOne(m => m.Product)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // No permitir eliminar un proveedor si tiene productos asociados

        }
    }
}
