using AutoMapper;
using ProductApp.Application.Extensions;
using ProductApp.Application.Filtering;
using ProductApp.Application.Interfaces.Filtering;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.Users.GetAllWithFilterUsers;

public class GetAllWithFilterUsersQueryHandler : IPaginatedQueryHandler<GetAllWithFilterUsersQuery, List<UserViewDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IFilterService<Product> _filterService;

    public GetAllWithFilterUsersQueryHandler(
        IProductRepository productRepository,
        IMapper mapper,
        IFilterService<Product> filterService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _filterService = filterService;
    }

    public async Task<PaginatedResponse<List<UserViewDto>>> Handle(GetAllWithFilterUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.Query();

        List<FilterCriteria> filters = new List<FilterCriteria>();

        if (!string.IsNullOrWhiteSpace(request.Name))
            filters.Add(new FilterCriteria { Field = "Name", Operator = "==", Value = request.Name });

        if (request.Value.HasValue)
            filters.Add(new FilterCriteria { Field = "Value", Operator = "==", Value = request.Value.Value });

        if (request.Quantity.HasValue)
            filters.Add(new FilterCriteria { Field = "Quantity", Operator = "==", Value = request.Quantity.Value });

        if (request.Status != 0)
            filters.Add(new FilterCriteria { Field = "Status", Operator = "==", Value = request.Status });

        query = _filterService.ApplyFilters(query, filters);

        List<Product> products = query.ToList();

        if (products.Count == 0)
        {
            return PaginatedResponse<List<UserViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound,
                new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        products.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<UserViewDto> productViewDtos = _mapper.Map<List<UserViewDto>>(paginatedDatas);

        return PaginatedResponse<List<UserViewDto>>.SuccessPaginatedDataWithMessage(
            productViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);
    }
}


