using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.RefreshTokens.GetAllRefreshTokens;

public class GetAllRefreshTokensQuery : IPaginatedQuery<List<RefreshTokenViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}


