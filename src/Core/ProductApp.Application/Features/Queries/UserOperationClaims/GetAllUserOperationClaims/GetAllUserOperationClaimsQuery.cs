using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.UserOperationClaims.GetAllUserOperationClaims;

public class GetAllUserOperationClaimsQuery : IPaginatedQuery<List<UserOperationClaimViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

