using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Extensions;
using ProductApp.Application.Filters;
using ProductApp.Application.Interfaces.Filters;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.Products.GetAllWithFilterProducts;

public class GetAllWithFilterProductsQueryHandler : IPaginatedQueryHandler<GetAllWithFilterProductsQuery, List<ProductViewDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IFilterService<Product> _filterService;

    public GetAllWithFilterProductsQueryHandler(IProductRepository productRepository, IMapper mapper, IFilterService<Product> filterService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _filterService = filterService;
    }

    public async Task<PaginatedResponse<List<ProductViewDto>>> Handle(GetAllWithFilterProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.Query();

        List<FilterCriteria> filters = new List<FilterCriteria>();

        if (!string.IsNullOrWhiteSpace(request.Name))
            filters.Add(new FilterCriteria { Field = "Name", Operator = "==", Value = request.Name });

        if (request.Value.HasValue)
            filters.Add(new FilterCriteria { Field = "Value", Operator = "==", Value = request.Value.Value });

        if (request.Quantity.HasValue)
            filters.Add(new FilterCriteria { Field = "Quantity", Operator = "==", Value = request.Quantity.Value });

        if (request.Status.HasValue)
            filters.Add(new FilterCriteria { Field = "Status", Operator = "==", Value = request.Status.Value });

        query = _filterService.ApplyFilters(query, filters);

        List<Product> products = query.ToList();

        if (products.Count == 0)
        {
            return PaginatedResponse<List<ProductViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound,
                new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        products.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<ProductViewDto> productViewDtos = _mapper.Map<List<ProductViewDto>>(paginatedDatas);

        return PaginatedResponse<List<ProductViewDto>>.SuccessPaginatedDataWithMessage(
            productViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);
    }
}


