
using AutoMapper;
using GestionInventario.src.Core.AutoMapperPrf;
using GestionInventario.src.Data;
using GestionInventario.src.Modules.Auths.Repositories;
using GestionInventario.src.Modules.Auths.Services;
using GestionInventario.src.Modules.Categories.Repositories;
using GestionInventario.src.Modules.Categories.Services;
using GestionInventario.src.Modules.ProductCategories.Repositories;
using GestionInventario.src.Modules.ProductCategories.Services;
using GestionInventario.src.Modules.Products.Repositories;
using GestionInventario.src.Modules.Products.Services;
using GestionInventario.src.Modules.Suppliers.Repositories;
using GestionInventario.src.Modules.Suppliers.Services;
using GestionInventario.src.Modules.Users.Domains.Models;
using GestionInventario.src.Modules.Users.Repositories;
using GestionInventario.src.Modules.Users.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = new MapperConfiguration(cfg =>
{
    cfg.AllowNullCollections = true;
    cfg.AllowNullDestinationValues = true;
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//controller
builder.Services.AddControllers();

//mapper
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

// Add repositories to the container
builder.Services.AddScoped<IUserRepository, UserRepositoryBD>();
builder.Services.AddScoped<IAuthRepository, AuthRepositoryBD>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();


builder.Services.AddDbContext<MyDbContext> (options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddRouting(Options => Options.LowercaseUrls = true);

// Configure Swagger/OpenAPI for the application.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>{
    options.SignIn.RequireConfirmedAccount = false;
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MyDbContext>()
.AddDefaultTokenProviders();

builder.Configuration.AddJsonFile("Properties/appsettings.BDD.json", optional: true, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("BDD"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("*");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run the application asynchronously
await app.RunAsync();