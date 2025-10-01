using ProductApp.Application.Helpers;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Services;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessToken>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
    }

    public async Task<ServiceResponse<AccessToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        List<User> users = await _userRepository.GetAllAsync();
        User? user = users.FirstOrDefault(u => u.UserName == request.UserName);

        if (user == null || !HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedAccessException("Invalid username or password");

        string accessToken = _tokenService.GenerateAccessToken(user);
        RefreshToken refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.UserId = user.Id;

        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);

        return ServiceResponse<AccessToken>.SuccessDataWithMessage(new AccessToken() { RefreshToken = addedRefreshToken.Token, Token = accessToken }, Messages.Success);
    }
}