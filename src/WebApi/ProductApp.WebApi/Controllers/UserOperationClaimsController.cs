using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Application.Features.UserOperationClaims.CreateUserOperationClaim;
using ProductApp.Application.Features.UserOperationClaims.DeleteUserOperationClaim;
using ProductApp.Application.Features.UserOperationClaims.UpdateUserOperationClaim;
using ProductApp.Application.Features.UserOperationClaims.GetAllWithFilterUserOperationClaims;
using ProductApp.Application.Features.UserOperationClaims.GetByIdUserOperationClaim;
using ProductApp.Application.Features.UserOperationClaims.GetAllUserOperationClaims;

namespace ProductApp.WebApi.Controllers;

/// <summary>
/// UserOperationClaimsController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// UserOperationClaimsController
    /// </summary>
    /// <param name="mediator"></param>
    public UserOperationClaimsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GetAll
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<List<UserOperationClaimViewDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PaginatedResponse<List<UserOperationClaimViewDto>>))]
    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserOperationClaimsQuery request)
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<UserOperationClaimViewDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<UserOperationClaimViewDto>))]
    [HttpGet("[action]/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserOperationClaimQuery request)
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<List<UserOperationClaimViewDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PaginatedResponse<List<UserOperationClaimViewDto>>))]
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> GetAllWithFilter([FromBody] GetAllWithFilterUserOperationClaimsQuery request)
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
    public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand request)
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
    public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand request)
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
    public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
