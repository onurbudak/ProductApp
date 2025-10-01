using ProductApp.Domain.Common;

namespace ProductApp.Domain.Entities;

public class RefreshToken : BaseEntity<long>
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsExpired;
    public long UserId { get; set; }
    public User? User { get; set; }
}

