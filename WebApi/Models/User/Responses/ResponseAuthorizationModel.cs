namespace WebApi.Models.User.Responses;

public class ResponseAuthorizationModel
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}