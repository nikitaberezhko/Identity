using AutoMapper;
using Domain;
using Services.JwtProvider.Abstractions;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Contracts.User;

namespace Services.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IJwtProvider jwtProvider)
    : IUserService
{
    public async Task<Guid> CreateUser(CreateUserDto createUserDto)
    {
        var user = mapper.Map<User>(createUserDto);
        
        var id = await userRepository.AddAsync(user);
        return id;
    }
    
    public async Task<string?> AuthenticateUser(AuthenticateUserDto authenticateUserDto)
    {
        var authUser = mapper.Map<User>(authenticateUserDto);
        
        var user = await userRepository.GetByLogin(authUser);
        
        if (user == null || user.Password != authUser.Password || user.IsDeleted)
            return null;
        
        var token = jwtProvider.GenerateToken(user);
        
        return token;
    }

    public async Task<User?> DeleteUser(DeleteUserDto deleteUserDto)
    {
        var delUser = mapper.Map<DeleteUserDto, User>(deleteUserDto);
        return await userRepository.DeleteAsync(delUser.Id);
    }
}