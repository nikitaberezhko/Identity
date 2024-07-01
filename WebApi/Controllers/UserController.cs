using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Abstractions;
using Services.Services.Contracts.User;
using WebApi.Mapping;
using WebApi.Models;
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
    public async Task<ActionResult<CommonResponse<ResponseCreateUserModel>>> CreateAsync(
        RequestCreateUserModel model)
    {
        var id = await userService.CreateUser(mapper.Map<CreateUserDto>(model));

        return new CreatedResult(nameof(CreateAsync),
            new CommonResponse<ResponseCreateUserModel>
            {
                Data = new ResponseCreateUserModel { Id = id },
                Error = null
            });
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<CommonResponse<ResponseDeleteUserModel>>> DeleteAsync(
        RequestDeleteUserModel model)
    {
        var user = await userService.DeleteUser(mapper.Map<DeleteUserDto>(model));

        return new CommonResponse<ResponseDeleteUserModel>
        {
            Data = new ResponseDeleteUserModel
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId
            },
            Error = null
        };
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<CommonResponse<ResponseAuthenticateUserModel>>> AuthenticateAsync(
        RequestAuthenticateUserModel model)
    {
        var token = await userService.AuthenticateUser(
            mapper.Map<AuthenticateUserDto>(model));

        return new CommonResponse<ResponseAuthenticateUserModel>
        {
            Data = new ResponseAuthenticateUserModel { Token = token },
            Error = null
        };
    }

    [Authorize]
    [HttpPost("authorize")]
    public async Task<ActionResult<CommonResponse<ResponseAuthorizationModel>>> AuthorizeAsync(
        RequestAuthorizationUserModel model)
    {
        var result = await userService.AuthorizeUser(
            mapper.Map<AuthorizationUserDto>(model));

        return new CommonResponse<ResponseAuthorizationModel>
        {
            Data = new ResponseAuthorizationModel
            {
                UserId = result.userId,
                RoleId = result.roleId
            },
            Error = null
        };
    }
}