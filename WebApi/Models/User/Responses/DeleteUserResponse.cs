namespace WebApi.Mapping;

public class DeleteUserResponse
{
    public Guid Id { get; set; }
    
    public int RoleId { get; set; }
    
    public string Name { get; set; }
}