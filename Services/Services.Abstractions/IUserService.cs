using Domain;
using Services.Services.Contracts.User;

namespace Services.Services.Abstractions;

public interface IUserService
{
    public Task<Guid> CreateUser(CreateUserDto createUserDto);
    
    public Task<string?> AuthenticateUser(AuthenticateUserDto authenticateUserDto);
    
    public Task<User?> DeleteUser(DeleteUserDto deleteUserDto);
}