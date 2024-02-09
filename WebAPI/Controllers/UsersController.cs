using Business.Abstracts;
using Core.Entities.Security;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await userService.GetAllAsync());
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await userService.GetByIdAsync(id));
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] User user)
    {
        return Ok(await userService.AddAsync(user));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] User user)
    {
        return Ok(await userService.UpdateAsync(user));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userService.DeleteByIdAsync(id);
        return Ok();
    }
}
