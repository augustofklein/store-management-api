using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreManagement.Application.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreManagement.Application.Auth.Service
{
    public class JwtService: IJwtService
    {
        private readonly JwtSettings jwtSettings;

        public JwtService(IOptions<JwtSettings> options)
        {
            jwtSettings = options.Value;
        }
        public UserToken GenerateToken(string username, int companyId)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            // TODO: Include roles of the user
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim("companyId", companyId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiresInMinutes),
                signingCredentials: credentials
            );
            
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserToken(accessToken);
        }
    }
}
