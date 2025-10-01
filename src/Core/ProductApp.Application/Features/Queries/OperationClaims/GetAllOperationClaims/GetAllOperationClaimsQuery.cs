using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.OperationClaims.GetAllOperationClaims;

public class GetAllOperationClaimsQuery : IPaginatedQuery<List<OperationClaimViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

