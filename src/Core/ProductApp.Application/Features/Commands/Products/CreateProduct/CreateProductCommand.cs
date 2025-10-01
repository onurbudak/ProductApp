using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Commands.Products.CreateProduct;

public class CreateUserCommand : ICommand<bool>
{
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
}
