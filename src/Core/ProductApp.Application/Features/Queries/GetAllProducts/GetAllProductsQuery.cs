using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Parameters;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQuery : RequestParameter, IRequest<PaginatedResponse<List<ProductViewDto>>>
{
}
