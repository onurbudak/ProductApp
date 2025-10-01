using AutoMapper;
using ProductApp.Application.Dto;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.Products.GetByIdWithFilterProduct;

public class GetByIdWithFilterQueryHandler : IQueryHandler<GetByIdWithFilterQuery, ProductViewDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetByIdWithFilterQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ProductViewDto>> Handle(GetByIdWithFilterQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.Id);

        if (product is null)
        {
            return ServiceResponse<ProductViewDto>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }
        ProductViewDto productViewDto = _mapper.Map<ProductViewDto>(product);

        return ServiceResponse<ProductViewDto>.SuccessDataWithMessage(productViewDto, Messages.Success);
    }
}

