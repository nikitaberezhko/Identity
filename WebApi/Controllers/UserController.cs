using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
[Route("api/[controller]")]
public class UserController(
    IUserService userService,
    IMapper mapper) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<ResponseCreateUserModel>> CreateAsync(
        RequestCreateUserModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var id = await userService.CreateUser(mapper.Map<CreateUserDto>(model));
        if (id == Guid.Empty)
        {
            return new ConflictResult();
        }
        
        var response = new ResponseCreateUserModel() { Id = id };
        
        return new CreatedResult(nameof(CreateAsync), response);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<ResponseDeleteUserModel>> DeleteAsync(
        RequestDeleteUserModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var user = await userService.DeleteUser(mapper.Map<DeleteUserDto>(model));
        
        return new ActionResult<ResponseDeleteUserModel>(
            new ResponseDeleteUserModel()
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId
            });
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<ResponseAuthenticateUserModel>> AuthenticateAsync(
        RequestAuthenticateUserModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var token = await userService.AuthenticateUser(mapper.Map<AuthenticateUserDto>(model));

        if (token != null)
            return new ActionResult<ResponseAuthenticateUserModel>(
                new ResponseAuthenticateUserModel()
            {
                Token = token
            });

        return new UnauthorizedResult();
    }

    [Authorize]
    [HttpPost("authorize")]
    public async Task<ActionResult<ResponseAuthorizationModel>> AuthorizeAsync()
    {
        var model = HttpContext.Request.Headers.Authorization.ToArray();
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(model[0].Split()[1]);
        Claim[] claims = jwtSecurityToken.Claims.ToArray();
        
        return new ActionResult<ResponseAuthorizationModel>(
            new ResponseAuthorizationModel()
            {
                RoleId = int.Parse(claims[1].Value)
            });
    }
}