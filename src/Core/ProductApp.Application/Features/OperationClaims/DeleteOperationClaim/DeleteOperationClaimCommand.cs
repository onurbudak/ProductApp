using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.OperationClaims.DeleteOperationClaim;

public class DeleteOperationClaimCommand : ICommand<bool>
{
    public long Id { get; set; }
}
