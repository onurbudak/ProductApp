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
        Product product = _mapper.Map<Product>(request);
        Product response = await _productRepository.DeleteAsync(product);
        return ServiceResponse<bool>.SuccessDataWithMessage(true, "Başarılı");
    }
}

