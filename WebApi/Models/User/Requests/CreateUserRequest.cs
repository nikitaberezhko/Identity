namespace WebApi.Models.User.Requests;

public class CreateUserRequest
{
    public int RoleId { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
}