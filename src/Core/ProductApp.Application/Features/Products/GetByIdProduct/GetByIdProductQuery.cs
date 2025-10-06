using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Products.GetByIdProduct;

public class GetByIdProductQuery : IQuery<ProductViewDto>
{
    public long Id { get; set; }
}
