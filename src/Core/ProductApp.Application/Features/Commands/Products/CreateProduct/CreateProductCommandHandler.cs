using AutoMapper;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.Products.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    } 
    public async Task<ServiceResponse<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product mappedProduct = _mapper.Map<Product>(request);
        _ = await _productRepository.AddAsync(mappedProduct);
        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

