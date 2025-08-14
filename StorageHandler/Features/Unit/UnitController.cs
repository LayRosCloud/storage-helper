using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.Unit.CreateUnit;
using StorageHandler.Features.Unit.DeleteUnit;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Features.Unit.FindByIdUnit;
using StorageHandler.Features.Unit.GetUnits;
using StorageHandler.Features.Unit.UpdateUnitArchive;
using StorageHandler.Features.Unit.UpdateUnitName;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Unit;

[ApiController]
[Route("v1/units")]
[Produces("application/json")]
public class UnitController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Finds all units
    /// </summary>
    /// <remarks>
    /// Finds all units by name with pagination
    /// </remarks>
    /// <param name="query">query body</param>
    /// <response code="200">Get all units</response>
    /// <response code="400">Invalid pagination query</response>
    [ProducesResponseType(typeof(List<UnitResponseDto>), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [HttpGet]
    public async Task<IActionResult> FindAllAsync([FromQuery] FindAllUnitsQuery query)
    {
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("x-total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }

    /// <summary>
    /// Finds by id
    /// </summary>
    /// <remarks>
    /// Finds by id unit
    /// </remarks>
    /// <param name="unitId">unit id</param>
    /// <response code="200">Finds the unit</response>
    /// <response code="404">Unit is not found</response>
    [ProducesResponseType(typeof(UnitResponseDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpGet("{unitId}")]
    public async Task<IActionResult> FindByIdAsync(long unitId)
    {
        var query = new FindByIdUnitQuery(unitId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create unit
    /// </summary>
    /// <remarks>
    /// Create unit by name
    /// </remarks>
    /// <param name="command">command query</param> 
    /// <response code="201">Create the unit</response>
    /// <response code="400">Unit data is invalid</response>
    [ProducesResponseType(typeof(UnitResponseDto), 201)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUnitCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) {StatusCode = 201};
    }

    /// <summary>
    /// Update unit by name
    /// </summary>
    /// <remarks>
    /// Update unit by name if name is not exists
    /// </remarks>
    /// <param name="command">command query</param> 
    /// <param name="unitId">unit id</param> 
    /// <response code="200">Update the unit</response>
    /// <response code="400">Unit data is invalid</response>
    /// <response code="404">Unit data is not found</response>
    [ProducesResponseType(typeof(UnitResponseDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPatch("{unitId}/name")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateUnitNameCommand command, long unitId)
    {
        command.Id = unitId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Change unit archive
    /// </summary>
    /// <remarks>
    /// Change unit archive
    /// </remarks>
    /// <param name="unitId">unit id</param>  
    /// <response code="200">Update the unit</response>
    /// <response code="400">Unit data is invalid</response>
    /// <response code="404">Unit is not found</response>
    [ProducesResponseType(typeof(UnitResponseDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPatch("{unitId}/archive")]
    public async Task<IActionResult> UpdateArchiveAsync(long unitId)
    {
        var command = new UpdateUnitArchiveCommand(unitId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete unit by id
    /// </summary>
    /// <remarks>
    /// Delete unit by id
    /// </remarks>
    /// <param name="unitId">unit id</param>  
    /// <response code="200">Delete the unit</response>
    /// <response code="400">Unit data is invalid</response>
    /// <response code="404">Unit is not found</response>
    [ProducesResponseType(typeof(UnitResponseDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpDelete("{unitId}")]
    public async Task<IActionResult> DeleteAsync(long unitId)
    {
        var command = new DeleteByIdUnitCommand(unitId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}