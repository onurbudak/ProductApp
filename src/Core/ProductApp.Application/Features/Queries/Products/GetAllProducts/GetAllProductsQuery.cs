using ProductApp.Application.Dto;
using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Queries.Products.GetAllProducts;

public class GetAllProductsQuery : IPaginatedQuery<List<ProductViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
