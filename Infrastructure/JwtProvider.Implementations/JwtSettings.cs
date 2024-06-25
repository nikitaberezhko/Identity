namespace Infrastructure.JwtProvider.Implementations;

public class JwtSettings
{
    public string SecretKey { get; set; }
    
    public string Issuer { get; set; }
    
    public int Expiration { get; set; }
}