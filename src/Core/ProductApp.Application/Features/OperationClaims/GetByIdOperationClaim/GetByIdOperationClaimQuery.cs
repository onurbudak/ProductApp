using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.OperationClaims.GetByIdOperationClaim;

public class GetByIdOperationClaimQuery : IQuery<OperationClaimViewDto>
{
    public long Id { get; set; }
}

