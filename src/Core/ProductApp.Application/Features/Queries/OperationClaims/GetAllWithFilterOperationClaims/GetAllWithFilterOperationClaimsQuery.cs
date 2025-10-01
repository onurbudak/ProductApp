using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.OperationClaims.GetAllWithFilterOperationClaims;

public class GetAllWithFilterOperationClaimsQuery : IPaginatedQuery<List<OperationClaimViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Name { get; set; }
}
