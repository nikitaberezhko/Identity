using AutoMapper;
using Domain;
using FluentValidation;
using Services.JwtProvider.Abstractions;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Contracts.User;

namespace Services.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IJwtProvider jwtProvider,
    IValidator<CreateUserDto> createUserValidator,
    IValidator<AuthenticateUserDto> authenticateUserValidator,
    IValidator<DeleteUserDto> deleteUserValidator)
    : IUserService
{
    public async Task<Guid> CreateUser(CreateUserDto createUserDto)
    {
        var validationResult = await createUserValidator.ValidateAsync(createUserDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var user = mapper.Map<User>(createUserDto);
        
        var id = await userRepository.AddAsync(user);
        return id;
    }
    
    public async Task<string?> AuthenticateUser(AuthenticateUserDto authenticateUserDto)
    {
        var validationResult = await authenticateUserValidator.ValidateAsync(authenticateUserDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var authUser = mapper.Map<User>(authenticateUserDto);
        var user = await userRepository.GetByLogin(authUser);
        
        if (user == null || user.Password != authUser.Password || user.IsDeleted)
            return null;
        
        var token = jwtProvider.GenerateToken(user);
        return token;
    }

    public async Task<User?> DeleteUser(DeleteUserDto deleteUserDto)
    {
        var validationResult = await deleteUserValidator.ValidateAsync(deleteUserDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var delUser = mapper.Map<DeleteUserDto, User>(deleteUserDto);
        return await userRepository.DeleteAsync(delUser.Id);
    }
}