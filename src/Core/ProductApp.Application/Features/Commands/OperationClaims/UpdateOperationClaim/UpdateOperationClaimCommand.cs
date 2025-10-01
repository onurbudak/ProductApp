using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.OperationClaims.UpdateOperationClaim;

public class UpdateOperationClaimCommand : ICommand<bool>
{
    public long Id { get; set; }
    public string Name { get; set; }
}
