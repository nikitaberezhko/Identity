using Domain;
using Services.Services.Contracts.User;
using Services.Services.Contracts.User.Response;

namespace Services.Services.Abstractions;

public interface IUserService
{
    public Task<Guid> CreateUser(CreateUserDto createUserDto);
    
    public Task<string?> AuthenticateUser(AuthenticateUserDto authenticateUserDto);

    public Task<ResponseAuthorizationUserDto> AuthorizeUser(
        AuthorizationUserDto authorizationUserDto);
    
    public Task<User?> DeleteUser(DeleteUserDto deleteUserDto);
}