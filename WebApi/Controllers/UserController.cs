using AutoMapper;
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
    IMapper mapper)
{
    [HttpPost("create")]
    public async Task<ActionResult<ResponseCreateUserModel>> CreateAsync(RequestCreateUserModel model)
    {
        var id = await userService.CreateUser(mapper.Map<CreateUserDto>(model));

        var response = new ResponseCreateUserModel()
        {
            StatusCode = StatusCodes.Status201Created,
            Id = id
        };
        return new CreatedResult(nameof(CreateAsync), response);
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult> AuthenticateAsync(
        RequestAuthenticateUserModel model)
    {
        var token = await userService.AuthenticateUser(mapper.Map<AuthenticateUserDto>(model));

        if (token != null)
            return new OkObjectResult(token);

        return new UnauthorizedResult();
    }

    [HttpPost("authorize")]
    public Task<ActionResult> AuthorizeAsync(
        RequestAuthorizationUserModel model) =>
        Task.FromResult<ActionResult>(new OkResult());
}