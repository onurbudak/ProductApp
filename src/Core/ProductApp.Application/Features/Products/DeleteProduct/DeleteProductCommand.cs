using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Products.DeleteProduct;

public class DeleteProductCommand : ICommand<bool>
{
    public long Id { get; set; }
}
