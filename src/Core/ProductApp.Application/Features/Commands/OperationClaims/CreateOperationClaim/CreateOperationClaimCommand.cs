using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.OperationClaims.CreateOperationClaim;

public class CreateOperationClaimCommand : ICommand<bool>
{
    public string Name { get; set; }

}
