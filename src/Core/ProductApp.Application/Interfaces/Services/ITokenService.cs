using ProductApp.Domain.Entities;

namespace ProductApp.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken();
}
