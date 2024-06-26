namespace WebApi.Models.User.Requests;

public class RequestAuthenticateUserModel
{
    public string Login { get; set; }
    
    public string Password { get; set; }
}