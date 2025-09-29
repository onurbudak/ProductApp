using ProductApp.Application.Dto;
using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Queries.Products.GetByIdProduct;

public class GetByIdProductQuery : IQuery<ProductViewDto>
{
    public long Id { get; set; }
}
