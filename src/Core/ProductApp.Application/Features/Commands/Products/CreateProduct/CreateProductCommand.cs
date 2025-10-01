using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.Products.CreateProduct;

public class CreateProductCommand : ICommand<bool>
{
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
}
