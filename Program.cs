using GestionInventario.Repository;
using GestionInventario.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//controller
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserServices, UserServices>();

//add repositories to the container
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
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

app.Run();