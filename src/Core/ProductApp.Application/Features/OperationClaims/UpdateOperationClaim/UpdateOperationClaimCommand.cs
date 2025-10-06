using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.OperationClaims.UpdateOperationClaim;

public class UpdateOperationClaimCommand : ICommand<OperationClaim>
{
    public long Id { get; set; }
    public required string Name { get; set; }
}
