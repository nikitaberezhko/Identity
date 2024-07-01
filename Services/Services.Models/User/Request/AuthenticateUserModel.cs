namespace Services.Services.Models.User.Request;

public class AuthenticateUserModel
{
    public string Login { get; set; }
    
    public string Password { get; set; }
}