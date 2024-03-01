using Core.Abstracts;
using Entity.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await orderService.GetAllAsync());
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await orderService.GetByIdAsync(id));
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] OrderAddDto orderAddDto)
    {
        await orderService.AddAsync(orderAddDto);
        return Ok();
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderUpdateDto orderUpdateDto)
    {
        await orderService.UpdateAsync(id, orderUpdateDto);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await orderService.DeleteByIdAsync(id);
        return Ok();
    }
}