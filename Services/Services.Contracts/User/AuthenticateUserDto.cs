namespace Services.Services.Contracts.User;

public class AuthenticateUserDto
{
    public string Login { get; set; }
    
    public string Password { get; set; }
}