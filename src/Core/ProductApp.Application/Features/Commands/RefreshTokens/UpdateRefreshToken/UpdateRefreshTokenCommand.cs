using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.RefreshTokens.UpdateRefreshToken;

public class UpdateRefreshTokenCommand : ICommand<bool>
{
    public long Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public long UserId { get; set; }
}
