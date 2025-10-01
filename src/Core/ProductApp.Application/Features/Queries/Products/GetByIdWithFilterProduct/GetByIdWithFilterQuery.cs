using ProductApp.Application.Dto;
using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Queries.Products.GetByIdWithFilterProduct;

public class GetByIdWithFilterQuery : IQuery<ProductViewDto>
{
    public long Id { get; set; }
}
