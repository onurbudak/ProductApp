using ProductApp.Application.Messaging;

namespace ProductApp.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommand : ICommand<bool>
{
    public long Id { get; set; }
}
