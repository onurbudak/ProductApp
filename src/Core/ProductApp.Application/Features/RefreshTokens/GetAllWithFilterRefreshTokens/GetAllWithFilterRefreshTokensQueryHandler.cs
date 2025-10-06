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

namespace ProductApp.Application.Features.RefreshTokens.GetAllWithFilterRefreshTokens;

internal class GetAllWithFilterRefreshTokensQueryHandler : IPaginatedQueryHandler<GetAllWithFilterRefreshTokensQuery, List<RefreshTokenViewDto>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;
    private readonly IFilterService<RefreshToken> _filterService;

    public GetAllWithFilterRefreshTokensQueryHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper, IFilterService<RefreshToken> filterService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
        _filterService = filterService;
    }

    public async Task<PaginatedResponse<List<RefreshTokenViewDto>>> Handle(GetAllWithFilterRefreshTokensQuery request, CancellationToken cancellationToken)
    {
        var query = _refreshTokenRepository.Query();

        List<FilterCriteria> filters = new List<FilterCriteria>();

        if (!string.IsNullOrWhiteSpace(request.Token))
            filters.Add(new FilterCriteria { Field = "Token", Operator = "==", Value = request.Token });

        if (request.Expires.HasValue)
            filters.Add(new FilterCriteria { Field = "Expires", Operator = "==", Value = request.Expires.Value });

        if (request.UserId.HasValue)
            filters.Add(new FilterCriteria { Field = "UserId", Operator = "==", Value = request.UserId.Value });

        query = _filterService.ApplyFilters(query, filters);

        List<RefreshToken> refreshTokens = query.ToList();

        if (refreshTokens.Count == 0)
        {
            return PaginatedResponse<List<RefreshTokenViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound,
                new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        refreshTokens.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<RefreshTokenViewDto> refreshTokenViewDtos = _mapper.Map<List<RefreshTokenViewDto>>(paginatedDatas);

        return PaginatedResponse<List<RefreshTokenViewDto>>.SuccessPaginatedDataWithMessage(
            refreshTokenViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);
    }
}
