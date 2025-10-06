using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Products.UpdateProduct;

public class UpdateProductCommand : ICommand<Product>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }
}
