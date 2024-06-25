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
        var requestUser = mapper.Map<User>(authenticateUserDto);
        
        var user = await userRepository.GetByLogin(requestUser);
        
        if (user == null || user.Password != requestUser.Password )
            return null;
        
        var token = jwtProvider.GenerateToken(user);
        
        return token;
    }
}