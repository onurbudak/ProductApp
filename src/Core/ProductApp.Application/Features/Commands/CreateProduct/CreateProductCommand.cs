using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Commands.CreateProduct;

public class CreateProductCommand : ICommand<bool>
{
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
}
