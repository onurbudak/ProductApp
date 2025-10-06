using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.RefreshTokens.CreateRefreshToken;

public class CreateRefreshTokenCommand : ICommand<RefreshToken>
{
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
    public long UserId { get; set; }
}
