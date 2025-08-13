using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.Resource.CreateResource;
using StorageHandler.Features.Resource.DeleteResourceById;
using StorageHandler.Features.Resource.FindAllResource;
using StorageHandler.Features.Resource.FindByIdResource;
using StorageHandler.Features.Resource.UpdateResourceArchive;
using StorageHandler.Features.Resource.UpdateResourceName;

namespace StorageHandler.Features.Resource;

[ApiController]
[Route("v1/resources")]
public class ResourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAllAsync([FromQuery] FindAllResourcesQuery query)
    {
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("x-total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }

    [HttpGet("{resourceId}")]
    public async Task<IActionResult> FindByIdAsync(long resourceId)
    {
        var query = new FindByIdResourceQuery(resourceId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateResourceCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    [HttpPatch("{resourceId}/name")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateResourceNameCommand command, long resourceId)
    {
        command.Id = resourceId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch("{resourceId}/archive")]
    public async Task<IActionResult> UpdateArchiveAsync(long resourceId)
    {
        var command = new UpdateResourceArchiveCommand(resourceId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{resourceId}")]
    public async Task<IActionResult> DeleteAsync(long resourceId)
    {
        var command = new DeleteResourceByIdCommand(resourceId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}