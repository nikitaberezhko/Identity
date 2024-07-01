namespace Services.Services.Contracts.User.Response;

public class ResponseAuthorizationUserDto
{
    public Guid UserId { get; set; }
    
    public int RoleId { get; set; }
}