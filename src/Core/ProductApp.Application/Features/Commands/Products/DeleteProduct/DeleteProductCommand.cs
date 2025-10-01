using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.Products.DeleteProduct;

public class DeleteProductCommand : ICommand<bool>
{
    public long Id { get; set; }
}
