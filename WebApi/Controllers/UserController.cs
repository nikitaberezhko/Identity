using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Abstractions;
using Services.Services.Models.User.Request;
using WebApi.Models;
using WebApi.Models.User.Requests;
using WebApi.Models.User.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/users")]
[ApiVersion(1.0)]
public class UserController(
    IUserService userService,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CommonResponse<CreateUserResponse>>> CreateAsync(
        CreateUserRequest request)
    {
        var id = await userService.CreateUser(mapper.Map<CreateUserModel>(request));

        var response = new CreatedResult(nameof(CreateAsync),
            new CommonResponse<CreateUserResponse>
            {
                Data = new CreateUserResponse { Id = id },
            });
        
        return response;
    }

    [HttpDelete]
    public async Task<ActionResult<CommonResponse<DeleteUserResponse>>> DeleteAsync(
        DeleteUserRequest request)
    {
        var user = await userService.DeleteUser(mapper.Map<DeleteUserModel>(request));

        var response = new CommonResponse<DeleteUserResponse>
        {
            Data = new DeleteUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId
            }
        };
        
        return response;
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<CommonResponse<AuthenticateUserResponse>>> AuthenticateAsync(
        AuthenticateUserRequest request)
    {
        var token = await userService.AuthenticateUser(
            mapper.Map<AuthenticateUserModel>(request));

        var response = new CommonResponse<AuthenticateUserResponse>
        {
            Data = new AuthenticateUserResponse { Token = token }
        };
        return response;
    }

    [Authorize]
    [HttpPost("authorize")]
    public async Task<ActionResult<CommonResponse<AuthorizationResponse>>> AuthorizeAsync(
        AuthorizationUserRequest request)
    {
        var result = await userService.AuthorizeUser(
            mapper.Map<AuthorizationUserModel>(request));

        var authModel = mapper.Map<AuthorizationResponse>(result);
        var response = new CommonResponse<AuthorizationResponse>
        {
            Data = new AuthorizationResponse
            {
                UserId = authModel.UserId,
                RoleId = authModel.RoleId
            }
        };
        
        return response;
    }
}