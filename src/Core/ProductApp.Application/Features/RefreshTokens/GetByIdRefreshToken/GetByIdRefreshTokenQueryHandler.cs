using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.RefreshTokens.GetByIdRefreshToken;

public class GetByIdRefreshTokenQueryHandler : IQueryHandler<GetByIdUserRefreshTokenQuery, RefreshTokenViewDto>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public GetByIdRefreshTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<RefreshTokenViewDto>> Handle(GetByIdUserRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetByIdAsync(request.Id);

        if (refreshToken is null)
        {
            return ServiceResponse<RefreshTokenViewDto>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }
        RefreshTokenViewDto refreshTokenViewDto = _mapper.Map<RefreshTokenViewDto>(refreshToken);

        return ServiceResponse<RefreshTokenViewDto>.SuccessDataWithMessage(refreshTokenViewDto, Messages.Success);
    }
}
