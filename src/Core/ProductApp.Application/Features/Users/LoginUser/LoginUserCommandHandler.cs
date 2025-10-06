using AutoMapper;
using MediatR;
using ProductApp.Application.Common;
using ProductApp.Application.Exceptions;
using ProductApp.Application.Features.RefreshTokens.CreateRefreshToken;
using ProductApp.Application.Features.Users.GetAllWithFilterUsers;
using ProductApp.Application.Helpers;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Services;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessToken>
{
    private readonly ITokenService _tokenService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(ITokenService tokenService, IMediator mediator, IMapper mapper)
    {
        _tokenService = tokenService;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<AccessToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        PaginatedResponse<List<UserViewDto>> paginatedResponse = await _mediator.Send(new GetAllWithFilterUsersQuery() { UserName = request.UserName }, cancellationToken);
        List<User> mappedUsers = _mapper.Map<List<User>>(paginatedResponse.Data);
        User? currentUser = mappedUsers.FirstOrDefault();

        if (currentUser == null || !HashingHelper.VerifyPasswordHash(request.Password, currentUser.PasswordHash, currentUser.PasswordSalt))
        {
            throw new UnauthorizedException(Messages.InvalidUserNameOrPassword);
        }

        string accessToken = _tokenService.GenerateAccessToken(currentUser);
        RefreshToken refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.UserId = currentUser.Id;
        ServiceResponse<RefreshToken> serviceResponseRefreshToken = await _mediator.Send(new CreateRefreshTokenCommand() { Expires = refreshToken.Expires, Token = refreshToken.Token, UserId = refreshToken.UserId }, cancellationToken);
        if (!serviceResponseRefreshToken.IsSuccess)
        {
            return ServiceResponse<AccessToken>.FailureDataWithMessage(Messages.Error, new Error(MessageCode.Error, Messages.Error));
        }

        return ServiceResponse<AccessToken>.SuccessDataWithMessage(new AccessToken() { RefreshToken = refreshToken.Token, Token = accessToken }, Messages.Success);
    }
}