using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommand : ICommand<bool>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }
}
