using ProductApp.Domain.Common;

namespace ProductApp.Domain.Entities;

public class User : BaseEntity<long>
{
    public required string Name { get; set; }
    public required string SurName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
    public List<UserOperationClaim> UserOperationClaims { get; set; } = new List<UserOperationClaim>();
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}

