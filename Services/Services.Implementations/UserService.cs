using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Domain;
using Exceptions.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.JwtProvider.Abstractions;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Contracts.User;

namespace Services.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IJwtProvider jwtProvider,
    IValidator<AuthenticateUserDto> authenticateUserValidator,
    IValidator<AuthorizationUserDto> authorizationUserValidator,
    IValidator<CreateUserDto> createUserValidator,
    IValidator<DeleteUserDto> deleteUserValidator)
    : IUserService
{
    public async Task<Guid> CreateUser(CreateUserDto createUserDto)
    {
        var validationResult = await createUserValidator.ValidateAsync(createUserDto);
        
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields: \nlogin{createUserDto.Login}" +
                          $"\npassword: {createUserDto.Password}" +
                          $"\nrole: {createUserDto.RoleId}" +
                          $"\nname: {createUserDto.Name} failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var user = mapper.Map<User>(createUserDto);
        
        var id = await userRepository.AddAsync(user);
        return id;
    }
    
    public async Task<string> AuthenticateUser(AuthenticateUserDto authenticateUserDto)
    {
        var validationResult = await authenticateUserValidator.ValidateAsync(authenticateUserDto);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields: \nlogin{authenticateUserDto.Login}" +
                          $"\npassword: {authenticateUserDto.Password} failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var authUser = mapper.Map<User>(authenticateUserDto);
        var user = await userRepository.GetByLogin(authUser);

        if (user.Password != authUser.Password)
            throw new ServiceException
            {
                Title = "Authentication failed",
                Message = "Wrong login or password",
                StatusCode = StatusCodes.Status401Unauthorized
            };
        
        if(user.IsDeleted)
            throw new ServiceException
            {
                Title = "Authentication failed",
                Message = "User not found",
                StatusCode = StatusCodes.Status401Unauthorized
            };
        
        var token = jwtProvider.GenerateToken(user);
        return token;
    }
    
    public async Task<(Guid userId, int roleId)> AuthorizeUser(
        AuthorizationUserDto authorizationUserDto)
    {
        var validationResult = 
            await authorizationUserValidator.ValidateAsync(authorizationUserDto);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this token: {authorizationUserDto.Token} failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var jwtSecurityToken = new JwtSecurityToken(authorizationUserDto.Token);
        var claims = jwtSecurityToken.Claims.ToArray();
        
        return (userId: Guid.Parse(claims[0].Value), 
            roleId: int.Parse(claims[1].Value));
    }

    public async Task<User?> DeleteUser(DeleteUserDto deleteUserDto)
    {
        var validationResult = await deleteUserValidator.ValidateAsync(deleteUserDto);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this id: {deleteUserDto.Id} failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var delUser = mapper.Map<DeleteUserDto, User>(deleteUserDto);
        return await userRepository.DeleteAsync(delUser.Id);
    }
}