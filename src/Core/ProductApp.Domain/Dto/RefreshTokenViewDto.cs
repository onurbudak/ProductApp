using ProductApp.Domain.Entities;

namespace ProductApp.Domain.Dto;

public class RefreshTokenViewDto
{
    public long Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsExpired;
    public long UserId { get; set; }
}
