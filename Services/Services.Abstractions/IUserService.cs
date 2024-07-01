using Services.Services.Models.User.Request;
using Services.Services.Models.User.Response;

namespace Services.Services.Abstractions;

public interface IUserService
{
    public Task<Guid> CreateUser(CreateUserModel createUserModel);
    
    public Task<string?> AuthenticateUser(AuthenticateUserModel authenticateUserModel);

    public Task<UserModel> AuthorizeUser(
        AuthorizationUserModel authorizationUserModel);
    
    public Task<UserModel> DeleteUser(DeleteUserModel deleteUserModel);
}