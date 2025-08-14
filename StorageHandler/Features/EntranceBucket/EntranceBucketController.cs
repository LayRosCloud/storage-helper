using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;
using StorageHandler.Features.EntranceBucket.CreateRangeEntranceBucket;
using StorageHandler.Features.EntranceBucket.DeleteEntranceBucket;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Features.EntranceBucket.FindAllEntranceBucketById;
using StorageHandler.Features.EntranceBucket.FindByIdEntranceBucket;
using StorageHandler.Features.EntranceBucket.UpdateEntranceBucket;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.EntranceBucket;

[ApiController]
[Route("v1/entrances/buckets")]
[Produces("application/json")]
public class EntranceBucketController : ControllerBase
{
    private readonly IMediator _mediator;

    public EntranceBucketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Finds all buckets by entrance id
    /// </summary>
    /// <remarks>
    /// Finds all buckets by entrance id with pagination
    /// </remarks>
    /// <param name="entranceId">entrance id</param>
    /// <param name="query">query body</param>
    /// <response code="200">Get all buckets by limit and page</response>
    /// <response code="400">Invalid pagination query</response>
    [ProducesResponseType(typeof(List<EntranceBucketFullDto>), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [HttpGet("/v1/entrances/{entranceId}/buckets")]
    public async Task<IActionResult> FindAllAsync([FromQuery] FindAllEntranceBucketByIdQuery query, long entranceId)
    {
        query.EntranceId = entranceId;
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("x-total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }

    /// <summary>
    /// Finds by id
    /// </summary>
    /// <remarks>
    /// Finds by id bucket
    /// </remarks>
    /// <param name="bucketId">bucket id</param>
    /// <response code="200">Get bucket</response>
    /// <response code="400">Invalid pagination query</response>
    /// <response code="404">Bucket with id is not found</response>
    [ProducesResponseType(typeof(EntranceBucketFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpGet("{bucketId}")]
    public async Task<IActionResult> FindByIdAsync(long bucketId)
    {
        var query = new FindByIdEntranceBucketQuery(bucketId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create bucket
    /// </summary>
    /// <remarks>
    /// Create bucket
    /// </remarks>
    /// <param name="command">request body</param>
    /// <response code="200">Create bucket</response>
    /// <response code="400">Invalid body</response>
    /// <response code="404">Referenced type with id is not found</response>
    [ProducesResponseType(typeof(EntranceBucketFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEntranceBucketCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    /// <summary>
    /// Create buckets
    /// </summary>
    /// <remarks>
    /// Create buckets
    /// </remarks>
    /// <param name="buckets">request body</param>
    /// <response code="200">Create buckets</response>
    /// <response code="400">Invalid body</response>
    /// <response code="404">Referenced type with id is not found</response>
    [ProducesResponseType(typeof(EntranceBucketFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPost("/range")]
    public async Task<IActionResult> CreateRangeAsync([FromBody] List<CreateEntranceBucketCommand> buckets)
    {
        var command = new CreateRangeEntranceBucketCommand(buckets);
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    /// <summary>
    /// Update bucket
    /// </summary>
    /// <remarks>
    /// Update bucket a quantity
    /// </remarks>
    /// <param name="bucketId">bucket id</param> 
    /// <param name="command">request body</param> 
    /// <response code="200">Update buckets</response>
    /// <response code="400">Invalid body</response>
    /// <response code="404">Referenced type with id is not found</response>
    [ProducesResponseType(typeof(EntranceBucketFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPatch("{bucketId}/quantity")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateEntranceBucketCommand command, long bucketId)
    {
        command.Id = bucketId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete bucket
    /// </summary>
    /// <remarks>
    /// Delete bucket by id
    /// </remarks>
    /// <param name="bucketId">bucket id</param>  
    /// <response code="200">Delete buckets</response>
    /// <response code="400">Invalid query</response>
    /// <response code="404">Referenced type with id is not found</response>
    [ProducesResponseType(typeof(EntranceBucketFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpDelete("{bucketId}")]
    public async Task<IActionResult> DeleteAsync(long bucketId)
    {
        var command = new DeleteEntranceBucketCommand(bucketId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}