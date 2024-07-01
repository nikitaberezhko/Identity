namespace Services.Services.Models.User.Request;

public class CreateUserModel
{
    public int RoleId { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
}