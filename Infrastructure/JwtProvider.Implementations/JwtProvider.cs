using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.JwtProvider.Abstractions;

namespace Infrastructure.JwtProvider.Implementations;

public class JwtProvider(IOptions<JwtSettings> jwtSettings) : IJwtProvider
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateToken(User user)
    {
        Claim[] claims = [
            new("userId", user.Id.ToString()),
            new("roleId", user.RoleId.ToString())
        ];

        var signingCredential = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredential,
            issuer: _jwtSettings.Issuer,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.Expiration)
            );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}