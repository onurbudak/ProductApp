using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Extensions;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.RefreshTokens.GetAllRefreshTokens;

public class GetAllRefreshTokensQueryHandler : IPaginatedQueryHandler<GetAllRefreshTokensQuery, List<RefreshTokenViewDto>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public GetAllRefreshTokensQueryHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<List<RefreshTokenViewDto>>> Handle(GetAllRefreshTokensQuery request, CancellationToken cancellationToken)
    {
        List<RefreshToken> refreshTokens = await _refreshTokenRepository.GetAllAsync();

        if (refreshTokens.Count == 0)
        {
            return PaginatedResponse<List<RefreshTokenViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        refreshTokens.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<RefreshTokenViewDto> refreshTokenViewDtos = _mapper.Map<List<RefreshTokenViewDto>>(paginatedDatas);

        return PaginatedResponse<List<RefreshTokenViewDto>>.SuccessPaginatedDataWithMessage(refreshTokenViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);

    }
}
