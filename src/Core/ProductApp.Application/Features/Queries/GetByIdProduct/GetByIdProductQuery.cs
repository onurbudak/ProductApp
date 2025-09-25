using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Queries.GetByIdProduct;

public class GetByIdProductQuery : IRequest<ServiceResponse<ProductViewDto>>
{
    public long Id { get; set; }
}
