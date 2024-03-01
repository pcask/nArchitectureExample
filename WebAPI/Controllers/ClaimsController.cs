using Core.Abstracts;
using Entity.DTOs.Claims;
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
    public async Task<IActionResult> Add([FromBody] ClaimAddDto claimAddDto)
    {
        await claimService.AddAsync(claimAddDto);
        return Ok();
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClaimUpdateDto claimUpdateDto)
    {
        await claimService.UpdateAsync(id, claimUpdateDto);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await claimService.DeleteByIdAsync(id);
        return Ok();
    }
}
