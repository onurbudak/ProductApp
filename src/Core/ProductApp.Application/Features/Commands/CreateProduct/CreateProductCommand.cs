using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ServiceResponse<bool>>
{
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
}
