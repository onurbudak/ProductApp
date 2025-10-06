using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.UserOperationClaims.UpdateUserOperationClaim;

public class UpdateUserOperationClaimCommand : ICommand<UserOperationClaim>
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long OperationClaimId { get; set; }
}
