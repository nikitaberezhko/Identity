namespace Services.Auth.Abstractions;

public interface IPasswordHasher
{
    public string GenerateHash(string password);

    public bool VerifyHash(string password, string hash);
}