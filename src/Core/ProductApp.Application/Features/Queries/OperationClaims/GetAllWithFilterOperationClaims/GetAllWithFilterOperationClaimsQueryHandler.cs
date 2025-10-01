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

namespace ProductApp.Application.Features.Queries.OperationClaims.GetAllWithFilterOperationClaims;

internal class GetAllWithFilterOperationClaimsQueryHandler : IPaginatedQueryHandler<GetAllWithFilterOperationClaimsQuery, List<OperationClaimViewDto>>
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;
    private readonly IFilterService<OperationClaim> _filterService;

    public GetAllWithFilterOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, IFilterService<OperationClaim> filterService)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
        _filterService = filterService;
    }

    public async Task<PaginatedResponse<List<OperationClaimViewDto>>> Handle(GetAllWithFilterOperationClaimsQuery request, CancellationToken cancellationToken)
    {
        var query = _operationClaimRepository.Query();

        List<FilterCriteria> filters = new List<FilterCriteria>();

        if (!string.IsNullOrWhiteSpace(request.Name))
            filters.Add(new FilterCriteria { Field = "Name", Operator = "==", Value = request.Name });

        query = _filterService.ApplyFilters(query, filters);

        List<OperationClaim> operationClaims = query.ToList();

        if (operationClaims.Count == 0)
        {
            return PaginatedResponse<List<OperationClaimViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound,
                new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        operationClaims.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<OperationClaimViewDto> operationClaimViewDtos = _mapper.Map<List<OperationClaimViewDto>>(paginatedDatas);

        return PaginatedResponse<List<OperationClaimViewDto>>.SuccessPaginatedDataWithMessage(
            operationClaimViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);
    }
}
