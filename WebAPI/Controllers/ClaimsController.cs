using Business.Abstracts;
using Core.Entities.Security;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClaimsController(IClaimService claimService) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await claimService.GetAllAsync());
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await claimService.GetByIdAsync(id));
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] Claim claim)
    {
        return Ok(await claimService.AddAsync(claim));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Claim claim)
    {
        return Ok(await claimService.UpdateAsync(claim));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await claimService.DeleteByIdAsync(id);
        return Ok();
    }
}
