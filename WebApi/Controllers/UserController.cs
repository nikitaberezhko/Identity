using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Abstractions;
using Services.Services.Contracts.User;
using WebApi.Models.User;
using WebApi.Models.User.Requests;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService,
    IMapper mapper) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<ResponseCreateUserModel>> CreateAsync(RequestCreateUserModel model)
    {
        var id = await userService.CreateUser(mapper.Map<CreateUserDto>(model));

        var response = new ResponseCreateUserModel() { Id = id };
        
        return new CreatedResult(nameof(CreateAsync), response);
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<ResponseAuthenticateUserModel>> AuthenticateAsync(
        RequestAuthenticateUserModel model)
    {
        var token = await userService.AuthenticateUser(mapper.Map<AuthenticateUserDto>(model));

        if (token != null)
            return new ActionResult<ResponseAuthenticateUserModel>(new ResponseAuthenticateUserModel()
            {
                Token = token
            });

        return new UnauthorizedResult();
    }

    [Authorize]
    [HttpPost("authorize")]
    public Task<ActionResult> AuthorizeAsync(
        RequestAuthorizationUserModel model) =>
        Task.FromResult<ActionResult>(new OkResult());
}