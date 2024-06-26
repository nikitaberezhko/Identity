namespace WebApi.Mapping;

public class ResponseDeleteUserModel
{
    public int StatusCode { get; set; }
    
    public Guid Id { get; set; }
    
    public Guid RoleId { get; set; }
    
    public string Name { get; set; }
}