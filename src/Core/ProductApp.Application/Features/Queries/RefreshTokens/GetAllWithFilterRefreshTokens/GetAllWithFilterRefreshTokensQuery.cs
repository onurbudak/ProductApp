using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.RefreshTokens.GetAllWithFilterRefreshTokens;

public class GetAllWithFilterRefreshTokensQuery : IPaginatedQuery<List<RefreshTokenViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Token { get; set; }
    public DateTime? Expires { get; set; }
    public long? UserId { get; set; }
}
