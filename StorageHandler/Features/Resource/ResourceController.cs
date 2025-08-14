using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageHandler.Features.Resource.CreateResource;
using StorageHandler.Features.Resource.DeleteResourceById;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Features.Resource.FindAllResource;
using StorageHandler.Features.Resource.FindByIdResource;
using StorageHandler.Features.Resource.UpdateResourceArchive;
using StorageHandler.Features.Resource.UpdateResourceName;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Resource;

[ApiController]
[Route("v1/resources")]
[Produces("application/json")]
public class ResourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Finds all resources
    /// </summary>
    /// <remarks>
    /// Finds all resources by name with pagination
    /// </remarks>
    /// <param name="query">query request</param>   
    /// <response code="200">Get all resources by limit and page</response>
    /// <response code="400">Invalid pagination query</response>
    [ProducesResponseType(typeof(List<ResourceFullDto>), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [HttpGet]
    public async Task<IActionResult> FindAllAsync([FromQuery] FindAllResourcesQuery query)
    {
        var result = await _mediator.Send(query);
        HttpContext.Response.Headers.Add("x-total-count", result.TotalCount.ToString());
        return Ok(result.Elements);
    }

    /// <summary>
    /// Finds by id resource
    /// </summary>
    /// <remarks>
    /// Finds by id resource
    /// </remarks>
    /// <param name="resourceId">resource id</param>    
    /// <response code="200">Get resource by id</response>
    /// <response code="400">Invalid id query</response>
    /// <response code="404">Resource is not found</response>
    [ProducesResponseType(typeof(ResourceFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpGet("{resourceId}")]
    public async Task<IActionResult> FindByIdAsync(long resourceId)
    {
        var query = new FindByIdResourceQuery(resourceId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create resource
    /// </summary>
    /// <remarks>
    /// Create resource by name
    /// </remarks>
    /// <param name="command">resource query body</param>     
    /// <response code="201">Create resource</response>
    /// <response code="400">Invalid data request</response>
    [ProducesResponseType(typeof(ResourceFullDto), 201)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateResourceCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(result) { StatusCode = 201 };
    }

    /// <summary>
    /// Update resource name
    /// </summary>
    /// <remarks>
    /// Update resource name
    /// </remarks>
    /// <param name="resourceId">resource id</param>   
    /// <param name="command">query body</param>   
    /// <response code="200">Update resource</response>
    /// <response code="400">Invalid data request</response>
    /// <response code="404">Resource with id is not found</response>
    [ProducesResponseType(typeof(ResourceFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPatch("{resourceId}/name")]
    public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateResourceNameCommand command, long resourceId)
    {
        command.Id = resourceId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Change resource archive
    /// </summary>
    /// <remarks>
    /// Change resource archive
    /// </remarks>
    /// <param name="resourceId">resource id</param>   
    /// <response code="200">Change resource archive</response>
    /// <response code="400">Invalid data request</response>
    /// <response code="404">Resource with id is not found</response>
    [ProducesResponseType(typeof(ResourceFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpPatch("{resourceId}/archive")]
    public async Task<IActionResult> UpdateArchiveAsync(long resourceId)
    {
        var command = new UpdateResourceArchiveCommand(resourceId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete resource
    /// </summary>
    /// <remarks>
    /// Delete resource by id
    /// </remarks>
    /// <param name="resourceId">resource id</param> 
    /// <response code="200">Delete resource by id</response>
    /// <response code="400">Invalid data request</response>
    /// <response code="404">Resource with id is not found</response>
    [ProducesResponseType(typeof(ResourceFullDto), 200)]
    [ProducesResponseType(typeof(ExceptionDto), 400)]
    [ProducesResponseType(typeof(ExceptionDto), 404)]
    [HttpDelete("{resourceId}")]
    public async Task<IActionResult> DeleteAsync(long resourceId)
    {
        var command = new DeleteResourceByIdCommand(resourceId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}