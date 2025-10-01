using ProductApp.Domain.Common;

namespace ProductApp.Domain.Entities;

public class User : BaseEntity<long>
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public List<UserOperationClaim> UserOperationClaims { get; set; } = new List<UserOperationClaim>();
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}

