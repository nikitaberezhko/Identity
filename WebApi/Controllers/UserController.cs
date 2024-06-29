using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Abstractions;
using Services.Services.Contracts.User;
using WebApi.Mapping;
using WebApi.Models.User;
using WebApi.Models.User.Requests;
using WebApi.Models.User.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class UserController(
    IUserService userService,
    IMapper mapper) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<ResponseCreateUserModel>> CreateAsync(
        RequestCreateUserModel model)
    {
        var id = await userService.CreateUser(mapper.Map<CreateUserDto>(model));
        if (id == Guid.Empty)
        {
            return new ConflictResult();
        }
  
        return new CreatedResult(nameof(CreateAsync),
            new ResponseCreateUserModel { Id = id });
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<ResponseDeleteUserModel>> DeleteAsync(
        RequestDeleteUserModel model)
    {
        var user = await userService.DeleteUser(mapper.Map<DeleteUserDto>(model));

        return new ResponseDeleteUserModel
        {
            Id = user.Id,
            Name = user.Name,
            RoleId = user.RoleId
        };
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<ResponseAuthenticateUserModel>> AuthenticateAsync(
        RequestAuthenticateUserModel model)
    {
        var token = await userService.AuthenticateUser(
            mapper.Map<AuthenticateUserDto>(model));

        if (token != null)
            return new ResponseAuthenticateUserModel
            {
                Token = token
            };

        return new UnauthorizedResult();
    }

    [Authorize]
    [HttpPost("authorize")]
    public async Task<ActionResult<ResponseAuthorizationModel>> AuthorizeAsync(
        RequestAuthorizationUserModel model)
    {
        var result = await userService.AuthorizeUser(
            mapper.Map<AuthorizationUserDto>(model));
        
        return new ResponseAuthorizationModel
        {
            UserId = result.userId,
            RoleId = result.roleId
        };
    }
}