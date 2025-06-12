using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Features.Commands.CreateProduct;
using ProductApp.Application.Features.Commands.DeleteProduct;
using ProductApp.Application.Features.Commands.UpdateProduct;
using ProductApp.Application.Features.Queries.GetAllProducts;
using ProductApp.Application.Features.Queries.GetByIdProduct;

namespace ProductApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GetList
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetAllProductsQuery request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
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
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand request)
    {
        var response = await _mediator.Send(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
