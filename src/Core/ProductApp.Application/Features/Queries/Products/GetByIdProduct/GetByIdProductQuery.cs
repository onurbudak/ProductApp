using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.Products.GetByIdProduct;

public class GetByIdProductQuery : IQuery<ProductViewDto>
{
    public long Id { get; set; }
}
