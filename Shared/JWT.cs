using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TEST.Shared
{
    public class JWT
    {
        public static string GenerarToken(string usuarioId, string secretKey, int minutosExpiracion = 120)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "evaluacion", // Cambia por tu issuer si lo necesitas
                audience: "evalucion-api", // Cambia por tu audience si lo necesitas
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutosExpiracion),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
