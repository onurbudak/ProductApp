using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.RefreshTokens.GetByIdRefreshToken;

public class GetByIdUserRefreshTokenQuery : IQuery<RefreshTokenViewDto>
{
    public long Id { get; set; }
}
