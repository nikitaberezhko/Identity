namespace Domain;

public class User
{
    public Guid Id { get; set; }
    
    public int RoleId { get; set; }
    
    public Role Role { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
    
    public bool IsDeleted { get; set; }
}