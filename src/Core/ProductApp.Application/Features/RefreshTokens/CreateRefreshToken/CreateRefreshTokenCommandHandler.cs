using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.RefreshTokens.CreateRefreshToken;

public class CreateRefreshTokenCommandHandler : ICommandHandler<CreateRefreshTokenCommand, RefreshToken>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public CreateRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<RefreshToken>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken mappedRefreshToken = _mapper.Map<RefreshToken>(request);
        RefreshToken refreshToken = await _refreshTokenRepository.AddAsync(mappedRefreshToken);
        return ServiceResponse<RefreshToken>.SuccessDataWithMessage(refreshToken, Messages.Success);
    }
}
