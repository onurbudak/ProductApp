using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.UserOperationClaims.GetByIdUserOperationClaim;

public class GetByIdUserOperationClaimQuery : IQuery<UserOperationClaimViewDto>
{
    public long Id { get; set; }
}
