using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.UserOperationClaims.GetAllWithFilterUserOperationClaims;

public class GetAllWithFilterUserOperationClaimsQuery : IPaginatedQuery<List<UserOperationClaimViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public long? UserId { get; set; }
    public long? OperationClaimId { get; set; }
}
