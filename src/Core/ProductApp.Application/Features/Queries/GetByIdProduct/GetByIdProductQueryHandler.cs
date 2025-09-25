using AutoMapper;
using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.GetByIdProduct;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ServiceResponse<ProductViewDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ProductViewDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.Id);
        ProductViewDto productViewDto = _mapper.Map<ProductViewDto>(product);
        return ServiceResponse<ProductViewDto>.SuccessDataWithMessage(productViewDto, "Başarılı");
    }
}

