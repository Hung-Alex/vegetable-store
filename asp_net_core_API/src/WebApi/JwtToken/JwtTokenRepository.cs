using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using store_vegetable.Core.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.JwtToken
{
    public class JwtTokenRepository : IJwtTokenRepository
    {
        private readonly IConfiguration _configuration;
        public JwtTokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateJwtToken(User user )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Issuer"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim("id",user.Id.ToString()),
        new Claim("role",user.Role)
      
        
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(4),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> GetInfoFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = tokenHandler.ReadJwtToken(token).Claims;
            var id=claims.FirstOrDefault(c => c.Type == "id").Value;
            return int.Parse( id);
        }

        public async Task<bool> IsJwtTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Issuer"]);

            try
            {
                
                var validatedToken = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                },  out SecurityToken validatedSecurityToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
