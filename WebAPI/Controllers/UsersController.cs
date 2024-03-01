using Core.Abstracts;
using Entity.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService, IUserClaimService userClaimService) : ControllerBase
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
    public async Task<IActionResult> Add([FromBody] UserAddDto userAddDto)
    {
        await userService.AddAsync(userAddDto);
        return Ok();
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto userUpdateDto)
    {
        await userService.UpdateAsync(id, userUpdateDto);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userService.DeleteByIdAsync(id);
        return Ok();
    }
}
