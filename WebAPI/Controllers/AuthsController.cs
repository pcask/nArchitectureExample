using Business.Abstracts;
using Core.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController(IAuthService authService) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        await authService.RegisterAsync(userRegisterDto);
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = await authService.LoginAsync(userLoginDto);
        return Ok(token);
    }
}
