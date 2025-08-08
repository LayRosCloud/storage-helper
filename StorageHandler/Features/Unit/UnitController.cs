using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.Unit.CreateUnit;
using StorageHandler.Features.Unit.DeleteUnit;
using StorageHandler.Features.Unit.FindByIdUnit;
using StorageHandler.Features.Unit.GetUnits;
using StorageHandler.Features.Unit.UpdateUnitArchive;
using StorageHandler.Features.Unit.UpdateUnitName;

namespace StorageHandler.Features.Unit;

[ApiController]
[Route("v1/units")]
public class UnitController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAllAsync([FromQuery] GetUnitsQuery query)
    {
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("X-Total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }
    
    [HttpGet("{unitId}")]
    public async Task<IActionResult> FindByIdAsync(long unitId)
    {
        var query = new FindByIdUnitQuery(unitId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUnitCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) {StatusCode = 201};
    }

    [HttpPatch("{unitId}/name")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateUnitNameCommand command, long unitId)
    {
        command.Id = unitId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch("{unitId}/archive")]
    public async Task<IActionResult> UpdateArchiveAsync(long unitId)
    {
        var command = new UpdateUnitArchiveCommand(unitId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{unitId}")]
    public async Task<IActionResult> DeleteAsync(long unitId)
    {
        var command = new DeleteByIdUnitCommand(unitId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}