using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.UserOperationClaims.DeleteUserOperationClaim;

public class DeleteUserOperationClaimCommand : ICommand<bool>
{
    public long Id { get; set; }
}
