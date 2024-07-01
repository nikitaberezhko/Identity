namespace Services.Services.Contracts.User;

public class CreateUserDto
{
    public int RoleId { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
}