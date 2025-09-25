using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<ServiceResponse<bool>>
{
    public long Id { get; set; }
}
