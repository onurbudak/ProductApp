using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.RefreshTokens.UpdateRefreshToken;

public class UpdateRefreshTokenCommand : ICommand<RefreshToken>
{
    public long Id { get; set; }
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
    public long UserId { get; set; }
}
