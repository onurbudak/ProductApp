using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.OperationClaims.CreateOperationClaim;

public class CreateOperationClaimCommand : ICommand<OperationClaim>
{
    public required string Name { get; set; }

}
