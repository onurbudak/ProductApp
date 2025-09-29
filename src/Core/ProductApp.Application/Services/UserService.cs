using ProductApp.Application.Helpers;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<ServiceResponse<bool>> Register(RegisterUserCommand request, CancellationToken cancellationToken = default)
    {
        List<User> users = await _userRepository.GetAllAsync();
        if (users.Any(u => u.UserName == request.UserName))
            throw new InvalidOperationException("Username already exists");

        var user = new User
        {
            UserName = request.UserName,
            Name = request.Name,
            SurName = request.SurName,
            Email = request.Email
        };
        HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        User addedUser = await _userRepository.AddAsync(user);
        UserOperationClaim userOperationClaim = new UserOperationClaim { OperationClaimId = 1, UserId = addedUser.Id };
        UserOperationClaim addedUserOperationClaim = await _userOperationClaimRepository.AddAsync(userOperationClaim);

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
    public async Task<ServiceResponse<AccessToken>> Login(LoginUserCommand request, CancellationToken cancellationToken = default)
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
