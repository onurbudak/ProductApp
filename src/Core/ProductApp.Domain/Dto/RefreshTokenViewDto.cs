using ProductApp.Domain.Common;

namespace ProductApp.Domain.Dto;

public class RefreshTokenViewDto : IDto
{
    public long Id { get; set; }
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsExpired;
    public long UserId { get; set; }
}
