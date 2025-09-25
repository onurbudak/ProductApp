using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResponse<bool>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product mappedProduct = _mapper.Map<Product>(request);
        Product? product = await _productRepository.DeleteAsync(mappedProduct);

        return product is not null ? ServiceResponse<bool>.SuccessMessageWithData(true, "Başarılı") : ServiceResponse<bool>.ErrorMessageWithData(false, "Başarısız");
    }
}

