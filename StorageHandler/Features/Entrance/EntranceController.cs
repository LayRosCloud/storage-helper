using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.Entrance.CreateEntrance;
using StorageHandler.Features.Entrance.DeleteEntrance;
using StorageHandler.Features.Entrance.FindAllEntrance;
using StorageHandler.Features.Entrance.FindByIdEntrance;
using StorageHandler.Features.Entrance.UpdateEntrance;

namespace StorageHandler.Features.Entrance;

[ApiController]
[Route("v1/entrances")]
public class EntranceController : ControllerBase
{
    private readonly IMediator _mediator;

    public EntranceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAllAsync([FromQuery] FindAllEntranceQuery query)
    {
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("x-total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }

    [HttpGet("{entranceId}")]
    public async Task<IActionResult> FindByIdAsync(long entranceId)
    {
        var query = new FindByIdEntranceQuery(entranceId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEntranceCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    [HttpPatch("{entranceId}/number")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateEntranceCommand command, long entranceId)
    {
        command.Id = entranceId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{entranceId}")]
    public async Task<IActionResult> DeleteAsync(long entranceId)
    {
        var command = new DeleteEntranceCommand(entranceId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}