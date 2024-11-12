
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionInventario.src.Modules.Auths.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GestionInventario.src.Modules.Auths.Services;


public class AuthService (UserManager<User> userManager, IConfiguration configuration
) : IAuthService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
  
    public async Task<AuthResponse> ValidateUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if(user == null)  
            return new AuthResponse 
            {
                 IsSuccessful = false,
                 Message = "Correo y contraseña inválidos." 
            };
       
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if(!isPasswordValid) 
            return new AuthResponse 
            {
                 IsSuccessful = false,
                 Message = "Correo y contraseña inválidos." 
            };
        
        var jwt = GenerateJwtToken(user);
        return new AuthResponse 
        {
            IsSuccessful = true,
            Message = "Usuario autenticado.",
            Jwt = jwt
        };
    }

    private string GenerateJwtToken(User user)
    {
    if (user == null)
        throw new ArgumentNullException(nameof(user), "User cannot be null.");

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    // Obtener la clave secreta de la configuración
    var secretKey = _configuration["Jwt:SecretKey"] ?? GenerateSecretKey(); // Generar una clave secreta si no está configurada

    // Validar que la clave secreta sea lo suficientemente larga (opcional, pero recomendado)
    if (secretKey.Length < 16) 
        throw new ArgumentException("The secret key should be at least 16 characters long.");

    // Crear la clave de seguridad usando la clave secreta
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    // Validar los valores de Issuer y Audience
    var issuer = _configuration["Jwt:Issuer"]?? "My issuer";
    var audience = _configuration["Jwt:Audience"] ?? "My audience";

 
    // Crear el token JWT
    var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1), // Usar UTC
        signingCredentials: creds
    );

    // Generar y devolver el token como una cadena
    return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Método para generar una clave secreta
    private static string GenerateSecretKey()
    {
        // Generar una clave secreta aleatoria de 32 bytes (256 bits)
        var secretKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        // Si prefieres una longitud específica, como 16 bytes (128 bits), ajusta el código según sea necesario.
        return secretKey;
    }

}