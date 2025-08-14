using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.Entrance.CreateEntrance;
using StorageHandler.Features.Entrance.DeleteEntrance;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Features.Entrance.FindAllEntrance;
using StorageHandler.Features.Entrance.FindByIdEntrance;
using StorageHandler.Features.Entrance.UpdateEntrance;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Entrance;

[ApiController]
[Route("v1/entrances")]
[Produces("application/json")]
public class EntranceController : ControllerBase
{
    private readonly IMediator _mediator;

    public EntranceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Finds all entrances
    /// </summary>
    /// <remarks>
    /// Finds all entrances by number with pagination
    /// </remarks>
    /// <param name="query">query body</param>  
    /// <response code="200">Get all entrances by limit and page</response>
    /// <response code="400">Invalid pagination query</response>
    [ProducesResponseType(typeof(List<EntranceFullDto>), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [HttpGet]
    public async Task<IActionResult> FindAllAsync([FromQuery] FindAllEntranceQuery query)
    {
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("x-total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }

    /// <summary>
    /// Finds by id entrance
    /// </summary>
    /// <remarks>
    /// Finds entrance
    /// </remarks>
    /// <param name="entranceId">entrance id</param> 
    /// <response code="200">Finds by id entrance</response>
    /// <response code="400">Invalid data request</response>
    /// <response code="404">Entrance with id is not found</response>
    [ProducesResponseType(typeof(EntranceFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpGet("{entranceId}")]
    public async Task<IActionResult> FindByIdAsync(long entranceId)
    {
        var query = new FindByIdEntranceQuery(entranceId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create entrance
    /// </summary>
    /// <remarks>
    /// Create entrance
    /// </remarks>
    /// <param name="command">request body</param>  
    /// <response code="201">Create entrance</response>
    /// <response code="400">Invalid data request</response>
    [ProducesResponseType(typeof(EntranceFullDto), 201)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEntranceCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    /// <summary>
    /// Update entrance
    /// </summary>
    /// <remarks>
    /// Update entrance
    /// </remarks>
    /// <param name="command">request body</param>   
    /// <param name="entranceId">entrance id</param>   
    /// <response code="200">Update entrance</response>
    /// <response code="400">Invalid data request</response>
    /// <response code="404">Entrance by id is not found</response>
    [ProducesResponseType(typeof(EntranceShortDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPatch("{entranceId}/number")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateEntranceCommand command, long entranceId)
    {
        command.Id = entranceId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete entrance
    /// </summary>
    /// <remarks>
    /// Delete entrance
    /// </remarks>
    /// <param name="entranceId">entrance id</param>  
    /// <response code="200">Delete entrance</response>
    /// <response code="400">Invalid data request</response>
    /// <response code="404">Entrance by id is not found</response>
    [ProducesResponseType(typeof(EntranceShortDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpDelete("{entranceId}")]
    public async Task<IActionResult> DeleteAsync(long entranceId)
    {
        var command = new DeleteEntranceCommand(entranceId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}