
using System.Reflection;
using System.Text;
using AutoMapper;
using GestionInventario.src.Core.AutoMapperPrf;
using GestionInventario.src.Data;
using GestionInventario.src.Modules.Addresses.Repositories;
using GestionInventario.src.Modules.Addresses.Services;
using GestionInventario.src.Modules.Auths.Config;
using GestionInventario.src.Modules.Auths.Repositories;
using GestionInventario.src.Modules.Auths.Services;
using GestionInventario.src.Modules.Categories.Repositories;
using GestionInventario.src.Modules.Categories.Services;
using GestionInventario.src.Modules.Movements.Repositories;
using GestionInventario.src.Modules.Movements.Services;
using GestionInventario.src.Modules.ProductCategories.Repositories;
using GestionInventario.src.Modules.ProductCategories.Services;
using GestionInventario.src.Modules.Products.Repositories;
using GestionInventario.src.Modules.Products.Services;
using GestionInventario.src.Modules.Roles.Repositories;
using GestionInventario.src.Modules.Roles.Services;
using GestionInventario.src.Modules.Suppliers.Repositories;
using GestionInventario.src.Modules.Suppliers.Services;
using GestionInventario.src.Modules.Users.Domains.Models;
using GestionInventario.src.Modules.Users.Repositories;
using GestionInventario.src.Modules.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<IMovementManualService, MovementManualService>();
builder.Services.AddScoped<IMovementSupplierService, MovementSupplierService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAddressService, AddressService>();

// Add repositories to the container
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddSingleton<JwtToken>();

builder.Services.AddDbContext<MyDbContext> (options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddRouting(Options => Options.LowercaseUrls = true);

// Configure Swagger/OpenAPI for the application.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        // Configurar Swagger para usar comentarios XML
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

builder.Services.AddDefaultIdentity<User>(options =>{
    options.SignIn.RequireConfirmedAccount = false;
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MyDbContext>()
.AddDefaultTokenProviders();

builder.Configuration.AddJsonFile("Properties/appsettings.BDD.json", optional: true, reloadOnChange: true);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme   = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme      = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme               = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var secret = builder.Configuration.GetSection("JwtConfig:Secret").Value;
    if (string.IsNullOrEmpty(secret))
    {
        throw new InvalidOperationException("JWT Secret is not configured properly.");
    }
    var key = Encoding.ASCII.GetBytes(secret);

    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration.GetSection("JwtConfig:Issuer").Value,
        ValidIssuer = builder.Configuration.GetSection("JwtConfig:Audience").Value,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("BDD"))
{
     app.UseDeveloperExceptionPage();
     app.UseSwagger();
     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}

app.UseCors("*");
app.UseAuthorization();
app.MapControllers();

// Run the application asynchronously
await app.RunAsync();