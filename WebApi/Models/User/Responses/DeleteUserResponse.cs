namespace WebApi.Models.User.Responses;

public class DeleteUserResponse
{
    public Guid Id { get; set; }
    
    public int RoleId { get; set; }
    
    public string Name { get; set; }
}