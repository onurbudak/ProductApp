using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.RefreshTokens.UpdateRefreshToken;

public class UpdateRefreshTokenCommandHandler : ICommandHandler<UpdateRefreshTokenCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateRefreshTokenCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper, ILogger<UpdateRefreshTokenCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<ServiceResponse<bool>> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken mappedRefreshToken = _mapper.Map<RefreshToken>(request);
        RefreshToken? refreshToken = await _refreshTokenRepository.UpdateAsync(mappedRefreshToken);

        if (refreshToken is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}
