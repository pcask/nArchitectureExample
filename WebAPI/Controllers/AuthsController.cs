using Core.Abstracts;
using Entity.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController(IAuthService authService) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserAddDto userAddDto)
    {
        await authService.RegisterAsync(userAddDto);
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = await authService.LoginAsync(userLoginDto);
        return Ok(token);
    }
}
