using MediatR;
using ProductApp.Application.Wrappers;
using ProductApp.Application.Dto;
using ProductApp.Application.Parameters;

namespace ProductApp.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQuery : PaginatedRequest, IRequest<PaginatedResponse<List<ProductViewDto>>>
{
}
