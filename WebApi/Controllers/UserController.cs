using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Abstractions;
using Services.Services.Contracts.User;
using WebApi.Models.User;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateUserModel model)
    {
        var id = await _userService.CreateUser(_mapper.Map<CreateUserDto>(model));

            
        return new CreatedAtActionResult(nameof(CreateAsync), 
            nameof(UserController),
            new { Id = id }, 
            model);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(AuthenticateUserModel model)
    {
        return new AcceptedAtActionResult(nameof(AuthenticateAsync),
            nameof(UserController),
            null,
            model);
    }
}