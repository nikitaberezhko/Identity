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
[Route("api/v{v:apiVersion}/users/")]
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

        var response = new CreatedResult(nameof(CreateAsync),
            new CommonResponse<ResponseCreateUserModel>
            {
                Data = new ResponseCreateUserModel { Id = id },
            });
        
        return response;
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<CommonResponse<ResponseDeleteUserModel>>> DeleteAsync(
        RequestDeleteUserModel model)
    {
        var user = await userService.DeleteUser(mapper.Map<DeleteUserDto>(model));

        var response = new CommonResponse<ResponseDeleteUserModel>
        {
            Data = new ResponseDeleteUserModel
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId
            }
        };
        
        return response;
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<CommonResponse<ResponseAuthenticateUserModel>>> AuthenticateAsync(
        RequestAuthenticateUserModel model)
    {
        var token = await userService.AuthenticateUser(
            mapper.Map<AuthenticateUserDto>(model));

        var response = new CommonResponse<ResponseAuthenticateUserModel>
        {
            Data = new ResponseAuthenticateUserModel { Token = token }
        };
        return response;
    }

    [Authorize]
    [HttpPost("authorize")]
    public async Task<ActionResult<CommonResponse<ResponseAuthorizationModel>>> AuthorizeAsync(
        RequestAuthorizationUserModel model)
    {
        var result = await userService.AuthorizeUser(
            mapper.Map<AuthorizationUserDto>(model));

        var responseModel = mapper.Map<ResponseAuthorizationModel>(result);
        var response = new CommonResponse<ResponseAuthorizationModel>
        {
            Data = new ResponseAuthorizationModel
            {
                UserId = responseModel.UserId,
                RoleId = responseModel.RoleId
            }
        };
        
        return response;
    }
}