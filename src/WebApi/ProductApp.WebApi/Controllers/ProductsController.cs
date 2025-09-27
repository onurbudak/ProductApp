using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Dto;
using ProductApp.Application.Features.Commands.CreateProduct;
using ProductApp.Application.Features.Commands.DeleteProduct;
using ProductApp.Application.Features.Commands.UpdateProduct;
using ProductApp.Application.Features.Queries.GetAllProducts;
using ProductApp.Application.Features.Queries.GetByIdProduct;
using ProductApp.Application.Wrappers;

namespace ProductApp.WebApi.Controllers;

/// <summary>
/// ProductsController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// ProductsController
    /// </summary>
    /// <param name="mediator"></param>
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GetAll
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<List<ProductViewDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PaginatedResponse<List<ProductViewDto>>))]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQuery request)
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<ProductViewDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponse<ProductViewDto>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdProductQuery request)
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
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductCommand request)
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
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
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
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
