using ProductApp.Domain.Entities;

namespace ProductApp.Application.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken();
}
