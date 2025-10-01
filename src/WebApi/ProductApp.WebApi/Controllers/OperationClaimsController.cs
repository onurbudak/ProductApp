using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Features.Commands.OperationClaims.CreateOperationClaim;
using ProductApp.Application.Features.Commands.OperationClaims.DeleteOperationClaim;
using ProductApp.Application.Features.Commands.OperationClaims.UpdateOperationClaim;
using ProductApp.Application.Features.Queries.OperationClaims.GetAllOperationClaims;
using ProductApp.Application.Features.Queries.OperationClaims.GetByIdOperationClaim;
using ProductApp.Application.Features.Queries.OperationClaims.GetAllWithFilterOperationClaims;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;

namespace ProductApp.WebApi.Controllers;

/// <summary>
/// OperationClaimsController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// OperationClaimsController
    /// </summary>
    /// <param name="mediator"></param>
    public OperationClaimsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GetAll
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<List<OperationClaimViewDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PaginatedResponse<List<OperationClaimViewDto>>))]
    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] GetAllOperationClaimsQuery request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<OperationClaimViewDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<OperationClaimViewDto>))]
    [HttpGet("[action]/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimQuery request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// GetAllWithFilter
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<List<OperationClaimViewDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PaginatedResponse<List<OperationClaimViewDto>>))]
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> GetAllWithFilter([FromBody] GetAllWithFilterOperationClaimsQuery request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<bool>))]
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<bool>))]
    [HttpPut("[action]")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<bool>))]
    [HttpDelete("[action]")]
    [Authorize]
    public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
