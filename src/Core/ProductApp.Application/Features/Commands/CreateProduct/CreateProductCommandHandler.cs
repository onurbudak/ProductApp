using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<bool>>
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
        Product product = _mapper.Map<Product>(request);
        await _productRepository.AddAsync(product);
        return ServiceResponse<bool>.SuccessDataWithMessage(true, "Başarılı");
    }
}

