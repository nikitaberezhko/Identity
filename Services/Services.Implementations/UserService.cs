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
using Services.Services.Contracts.User.Response;

namespace Services.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
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
                Message = $"User with this fields failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var user = mapper.Map<User>(createUserDto);
        
        user.Password = passwordHasher.GenerateHash(user.Password);
        
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
                Message = $"User with this fields: login failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var authUser = mapper.Map<User>(authenticateUserDto);
        var user = await userRepository.GetByLogin(authUser);

        if (passwordHasher.VerifyHash(user.Password, user.Password))
            throw new ServiceException
            {
                Title = "Authentication failed",
                Message = "Wrong login or password",
                StatusCode = StatusCodes.Status401Unauthorized
            };
        
        if (user.IsDeleted)
            throw new ServiceException
            {
                Title = "Authentication failed",
                Message = "User not found",
                StatusCode = StatusCodes.Status401Unauthorized
            };
        
        var token = jwtProvider.GenerateToken(user);
        return token;
    }
    
    public async Task<ResponseAuthorizationUserDto> AuthorizeUser(
        AuthorizationUserDto authorizationUserDto)
    {
        var validationResult = 
            await authorizationUserValidator.ValidateAsync(authorizationUserDto);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this token failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var jwtSecurityToken = new JwtSecurityToken(authorizationUserDto.Token);
        var claims = jwtSecurityToken.Claims.ToArray();
        
        var result = new ResponseAuthorizationUserDto
        {
            UserId = Guid.Parse(claims[0].Value),
            RoleId = int.Parse(claims[1].Value)
        };
        
        return result;
    }

    public async Task<User?> DeleteUser(DeleteUserDto deleteUserDto)
    {
        var validationResult = await deleteUserValidator.ValidateAsync(deleteUserDto);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this id: failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var delUser = mapper.Map<DeleteUserDto, User>(deleteUserDto);
        return await userRepository.DeleteAsync(delUser.Id);
    }
}