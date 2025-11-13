using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.ApplicationLayer.Options; 
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BartugWeb.InfrastructureLayer.Services;

public class JwtService : IJwtService
{
    private readonly IOptions<JwtSettings> _jwtSettings;

    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(string userId, string username, string email)
    {
        var secretKey = _jwtSettings.Value.SecretKey
            ?? throw new InvalidOperationException("JWT SecretKey not configured");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Value.Issuer,
            audience: _jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.ExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}