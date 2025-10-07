using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.UserOperationClaims.CreateUserOperationClaim;

public class CreateUserOperationClaimCommand : ICommand<UserOperationClaim>
{
    public long UserId { get; set; }
    public long OperationClaimId { get; set; }
}
