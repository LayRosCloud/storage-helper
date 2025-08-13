using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;
using StorageHandler.Features.EntranceBucket.CreateRangeEntranceBucket;
using StorageHandler.Features.EntranceBucket.DeleteEntranceBucket;
using StorageHandler.Features.EntranceBucket.FindAllEntranceBucketById;
using StorageHandler.Features.EntranceBucket.FindByIdEntranceBucket;
using StorageHandler.Features.EntranceBucket.UpdateEntranceBucket;

namespace StorageHandler.Features.EntranceBucket;

[ApiController]
[Route("v1/entrances/buckets")]
public class EntranceBucketController : ControllerBase
{
    private readonly IMediator _mediator;

    public EntranceBucketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/v1/entrances/{entranceId}/buckets")]
    public async Task<IActionResult> FindAllAsync([FromQuery] FindAllEntranceBucketByIdQuery query, long entranceId)
    {
        query.EntranceId = entranceId;
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("x-total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }

    [HttpGet("{bucketId}")]
    public async Task<IActionResult> FindByIdAsync(long bucketId)
    {
        var query = new FindByIdEntranceBucketQuery(bucketId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEntranceBucketCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    [HttpPost("/range")]
    public async Task<IActionResult> CreateRangeAsync([FromBody] List<CreateEntranceBucketCommand> buckets)
    {
        var command = new CreateRangeEntranceBucketCommand(buckets);
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    [HttpPatch("{bucketId}/quantity")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateEntranceBucketCommand command, long bucketId)
    {
        command.Id = bucketId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{bucketId}")]
    public async Task<IActionResult> DeleteAsync(long bucketId)
    {
        var command = new DeleteEntranceBucketCommand(bucketId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}