using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.UserOperationClaims.CreateUserOperationClaim;

public class CreateUserOperationClaimCommand : ICommand<bool>
{
    public long UserId { get; set; }
    public long OperationClaimId { get; set; }
}
