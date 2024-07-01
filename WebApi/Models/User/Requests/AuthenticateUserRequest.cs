namespace WebApi.Models.User.Requests;

public class AuthenticateUserRequest
{
    public string Login { get; set; }
    
    public string Password { get; set; }
}