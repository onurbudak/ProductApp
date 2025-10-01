using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.UserOperationClaims.UpdateUserOperationClaim;

public class UpdateUserOperationClaimCommand : ICommand<bool>
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long OperationClaimId { get; set; }
}
