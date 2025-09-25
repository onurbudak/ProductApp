using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ServiceResponse<bool>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }
}
