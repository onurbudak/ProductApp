using ProductApp.Application.Dto;
using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Queries.Products.GetAllWithFilterProducts;

public class GetAllWithFilterProductsQuery : IPaginatedQuery<List<ProductViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }
}
