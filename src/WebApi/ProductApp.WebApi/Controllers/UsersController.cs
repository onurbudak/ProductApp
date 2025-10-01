using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Features.Commands.Users.LoginUser;
using ProductApp.Application.Features.Commands.Users.RegisterUser;
using ProductApp.Application.Services;
using ProductApp.Application.Wrappers;

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
    /// Register
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<bool>))]
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<AccessToken>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<AccessToken>))]
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

}
