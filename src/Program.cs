
using System.Reflection;
using System.Text;
using AutoMapper;
using GestionInventario.src.Core.AutoMapperPrf;
using GestionInventario.src.Data;
using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Repositories;
using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Services;
using GestionInventario.src.Modules.UsersRolesManagement.Auths.Config;
using GestionInventario.src.Modules.UsersRolesManagement.Auths.Repositories;
using GestionInventario.src.Modules.UsersRolesManagement.Auths.Services;
using GestionInventario.src.Modules.ProductsManagement.Categories.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Categories.Services;
using GestionInventario.src.Modules.ProductsManagement.Movements.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Movements.Services;
using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Repositories;
using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Services;
using GestionInventario.src.Modules.ProductsManagement.Products.Repositories;
using GestionInventario.src.Modules.ProductsManagement.Products.Services;
using GestionInventario.src.Modules.UsersRolesManagement.Roles.Repositories;
using GestionInventario.src.Modules.UsersRolesManagement.Roles.Services;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Repositories;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Services;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Repositories;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using GestionInventario.src.Modules.Notifications.Alerts.Services;
using GestionInventario.src.Modules.Notifications.Alerts.Domain.Dtos;
using GestionInventario.src.Modules.Notifications.Alerts.Repositories;

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
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IKardexCalculators, KardexCalculators>();
builder.Services.AddScoped<IStockAlertService,StockAlertService>();

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
builder.Services.AddScoped<IAlertRepository,AlertRepository>();

builder.Services.AddSingleton<JwtToken>();
builder.Services.AddHostedService<StockAlertBackgroundService>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddDbContext<MyDbContext> (options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddRouting(Options => Options.LowercaseUrls = true);

// Configure Swagger/OpenAPI for the application.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid JWT token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        
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
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

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
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Run the application asynchronously
await app.RunAsync();