using Core.Abstracts;
using Entity.DTOs.UserClaims;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserClaimsController(IUserClaimService userClaimService) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await userClaimService.GetAllAsync());
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await userClaimService.GetByIdAsync(id));
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] UserClaimAddDto addUserClaimDto)
    {
        await userClaimService.AddAsync(addUserClaimDto);
        return Ok();
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UserClaimUpdateDto updateUserClaimDto)
    {
        await userClaimService.UpdateAsync(updateUserClaimDto);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userClaimService.DeleteByIdAsync(id);
        return Ok();
    }
}