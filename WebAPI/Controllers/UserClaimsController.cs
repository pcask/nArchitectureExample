using Business.Abstracts;
using Core.DTOs.UserClaim;
using Core.Entities.Security;
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
    public async Task<IActionResult> Add([FromBody] AddUserClaimDto userClaimDto)
    {
        return Ok(await userClaimService.AddAsync(userClaimDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserClaimDto userClaimDto)
    {
        return Ok(await userClaimService.UpdateAsync(userClaimDto));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userClaimService.DeleteByIdAsync(id);
        return Ok();
    }
}