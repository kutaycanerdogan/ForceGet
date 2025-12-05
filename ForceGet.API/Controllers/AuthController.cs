using ForceGet.Application.DTOs;
using ForceGet.Infrastructure.DTOs;
using ForceGet.Infrastructure.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ForceGet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (model.Password != model.ConfirmPassword)
            return BadRequest("Passwords do not match");

        try
        {
            var user = await _userService.RegisterAsync(model.Email, model.Password);
            return Ok(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var token = await _userService.LoginAsync(model.Email, model.Password);
            return Ok(new TokenDto { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
