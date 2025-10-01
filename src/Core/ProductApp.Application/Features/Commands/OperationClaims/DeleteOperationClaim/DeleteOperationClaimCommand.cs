using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.OperationClaims.DeleteOperationClaim;

public class DeleteOperationClaimCommand : ICommand<bool>
{
    public long Id { get; set; }
}
