using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.RefreshTokens.DeleteRefreshToken;

public class DeleteRefreshTokenCommand : ICommand<bool>
{
    public long Id { get; set; }
}

