using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.RefreshTokens.CreateRefreshToken;

public class CreateRefreshTokenCommandHandler : ICommandHandler<CreateRefreshTokenCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public CreateRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken mappedRefreshToken = _mapper.Map<RefreshToken>(request);
        _ = await _refreshTokenRepository.AddAsync(mappedRefreshToken);
        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}
