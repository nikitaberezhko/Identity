namespace WebApi.Models.User;

public class CreateUserModel
{
    public Guid RoleId { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
}