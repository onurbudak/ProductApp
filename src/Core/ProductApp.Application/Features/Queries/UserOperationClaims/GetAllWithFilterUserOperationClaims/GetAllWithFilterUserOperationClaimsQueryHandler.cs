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

namespace ProductApp.Application.Features.Queries.UserOperationClaims.GetAllWithFilterUserOperationClaims;

public class GetAllWithFilterUserOperationClaimsQueryHandler : IPaginatedQueryHandler<GetAllWithFilterUserOperationClaimsQuery, List<UserOperationClaimViewDto>>
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;
    private readonly IFilterService<UserOperationClaim> _filterService;

    public GetAllWithFilterUserOperationClaimsQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, IFilterService<UserOperationClaim> filterService)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _mapper = mapper;
        _filterService = filterService;
    }

    public async Task<PaginatedResponse<List<UserOperationClaimViewDto>>> Handle(GetAllWithFilterUserOperationClaimsQuery request, CancellationToken cancellationToken)
    {
        var query = _userOperationClaimRepository.Query();

        List<FilterCriteria> filters = new List<FilterCriteria>();

        if (request.UserId.HasValue)
            filters.Add(new FilterCriteria { Field = "UserId", Operator = "==", Value = request.UserId.Value });

        if (request.OperationClaimId.HasValue)
            filters.Add(new FilterCriteria { Field = "OperationClaimId", Operator = "==", Value = request.OperationClaimId.Value });

        query = _filterService.ApplyFilters(query, filters);

        List<UserOperationClaim> userOperationClaims = query.ToList();

        if (userOperationClaims.Count == 0)
        {
            return PaginatedResponse<List<UserOperationClaimViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound,
                new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        userOperationClaims.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<UserOperationClaimViewDto> userOperationClaimViewDtos = _mapper.Map<List<UserOperationClaimViewDto>>(paginatedDatas);

        return PaginatedResponse<List<UserOperationClaimViewDto>>.SuccessPaginatedDataWithMessage(
            userOperationClaimViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);
    }
}

