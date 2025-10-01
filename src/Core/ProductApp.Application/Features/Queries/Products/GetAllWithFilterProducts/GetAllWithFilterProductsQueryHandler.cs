using AutoMapper;
using ProductApp.Application.Dto;
using ProductApp.Application.Extensions;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.Products.GetAllWithFilterProducts;

public class GetAllWithFilterProductsQueryHandler : IPaginatedQueryHandler<GetAllWithFilterProductsQuery, List<ProductViewDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllWithFilterProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<List<ProductViewDto>>> Handle(GetAllWithFilterProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = await _productRepository.GetAllWithFilterAsync(e =>
            request.Status != 0 && e.Status == request.Status &&
            !string.IsNullOrWhiteSpace(request.Name) && e.Name == request.Name &&
            request.Value.HasValue && e.Value == request.Value &&
            request.Quantity.HasValue && e.Quantity == request.Quantity);

        if (products.Count == 0)
        {
            return PaginatedResponse<List<ProductViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        products.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<ProductViewDto> productViewDtos = _mapper.Map<List<ProductViewDto>>(paginatedDatas);

        return PaginatedResponse<List<ProductViewDto>>.SuccessPaginatedDataWithMessage(productViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);

    }
}

