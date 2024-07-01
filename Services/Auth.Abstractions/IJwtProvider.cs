using Domain;

namespace Services.JwtProvider.Abstractions;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}