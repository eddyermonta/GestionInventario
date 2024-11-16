using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionInventario.src.Modules.Auths.Config;
using GestionInventario.src.Modules.Users.Domains.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GestionInventario.src.Modules.Auths.Services
{
 public class JwtToken(IOptions<JwtConfig> jwtConfig)
    {
        private readonly JwtConfig _jwtConfig = jwtConfig.Value;

        public string GenerateJwtToken(User user, List<string> userRoles)
        {

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret ?? string.Empty);
            
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),  // Convierte el Id a string si es necesario
                new (JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new (JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                IssuedAt = DateTime.UtcNow, // Fecha y hora de emisión
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}