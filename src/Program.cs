
using GestionInventario.src.Auths.Repositories;
using GestionInventario.src.Auths.Services;
using GestionInventario.src.Shared;
using GestionInventario.src.Users.Repositories;
using GestionInventario.src.Users.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//controller
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserServices, UserServices>();

// Add repositories to the container
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();

// Add shared services to the container
builder.Services.AddSingleton<IAuthUserCache, AuthUserCache>();

builder.Services.AddRouting(Options => Options.LowercaseUrls = true);

// Configure Swagger/OpenAPI for the application.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run the application asynchronously
await app.RunAsync();