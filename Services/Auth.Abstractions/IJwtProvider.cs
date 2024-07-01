using Domain;

namespace Services.Auth.Abstractions;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}