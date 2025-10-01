using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.RefreshTokens.DeleteRefreshToken;

public class DeleteRefreshTokenCommandHandler : ICommandHandler<DeleteRefreshTokenCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public DeleteRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken mappedRefreshToken = _mapper.Map<RefreshToken>(request);
        RefreshToken? refreshToken = await _refreshTokenRepository.DeleteAsync(mappedRefreshToken);

        if (refreshToken is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}
