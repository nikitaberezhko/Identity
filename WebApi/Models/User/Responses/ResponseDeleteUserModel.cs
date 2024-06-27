namespace WebApi.Mapping;

public class ResponseDeleteUserModel
{
    public Guid Id { get; set; }
    
    public int RoleId { get; set; }
    
    public string Name { get; set; }
}