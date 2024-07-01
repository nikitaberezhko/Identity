using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Domain;
using Exceptions.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Auth.Abstractions;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Models.User.Request;
using Services.Services.Models.User.Response;

namespace Services.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
    IValidator<AuthenticateUserModel> authenticateUserValidator,
    IValidator<AuthorizationUserModel> authorizationUserValidator,
    IValidator<CreateUserModel> createUserValidator,
    IValidator<DeleteUserModel> deleteUserValidator)
    : IUserService
{
    public async Task<Guid> CreateUser(CreateUserModel createUserModel)
    {
        var validationResult = await createUserValidator.ValidateAsync(createUserModel);
        
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var user = mapper.Map<User>(createUserModel);
        
        user.Password = passwordHasher.GenerateHash(user.Password);
        
        var id = await userRepository.AddAsync(user);
        return id;
    }
    
    public async Task<string> AuthenticateUser(AuthenticateUserModel authenticateUserModel)
    {
        var validationResult = await authenticateUserValidator
            .ValidateAsync(authenticateUserModel);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields: login failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var authUser = mapper.Map<User>(authenticateUserModel);
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
    
    public async Task<UserModel> AuthorizeUser(
        AuthorizationUserModel authorizationUserModel)
    {
        var validationResult = 
            await authorizationUserValidator.ValidateAsync(authorizationUserModel);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this token failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var jwtSecurityToken = new JwtSecurityToken(authorizationUserModel.Token);
        var claims = jwtSecurityToken.Claims.ToArray();
        
        var result = new UserModel
        {
            Id = Guid.Parse(claims[0].Value),
            RoleId = int.Parse(claims[1].Value)
        };
        
        return result;
    }

    public async Task<UserModel> DeleteUser(DeleteUserModel deleteUserModel)
    {
        var validationResult = await deleteUserValidator.ValidateAsync(deleteUserModel);

        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this id: failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var user = await userRepository.DeleteAsync(deleteUserModel.Id);

        var result = mapper.Map<UserModel>(user);
        return result;
    }
}