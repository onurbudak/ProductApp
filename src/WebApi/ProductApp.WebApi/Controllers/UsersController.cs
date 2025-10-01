using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Features.Commands.Users.CreateUser;
using ProductApp.Application.Features.Commands.Users.DeleteUser;
using ProductApp.Application.Features.Commands.Users.UpdateUser;
using ProductApp.Application.Features.Queries.Users.GetAllUsers;
using ProductApp.Application.Features.Queries.Users.GetByIdUser;
using ProductApp.Application.Features.Queries.Users.GetAllWithFilterUsers;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;

namespace ProductApp.WebApi.Controllers;

/// <summary>
/// UsersController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// UsersController
    /// </summary>
    /// <param name="mediator"></param>
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GetAll
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<List<UserViewDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PaginatedResponse<List<UserViewDto>>))]
    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery request)
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<UserViewDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<UserViewDto>))]
    [HttpGet("[action]/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserQuery request)
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<List<UserViewDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PaginatedResponse<List<UserViewDto>>))]
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> GetAllWithFilter([FromBody] GetAllWithFilterUsersQuery request)
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
    public async Task<IActionResult> Add([FromBody] CreateUserCommand request)
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
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand request)
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
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
