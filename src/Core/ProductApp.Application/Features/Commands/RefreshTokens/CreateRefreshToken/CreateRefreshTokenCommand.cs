using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.RefreshTokens.CreateRefreshToken;

public class CreateRefreshTokenCommand : ICommand<bool>
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public long UserId { get; set; }
}
