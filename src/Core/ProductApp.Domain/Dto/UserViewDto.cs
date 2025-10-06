using ProductApp.Domain.Common;

namespace ProductApp.Domain.Dto;

public class UserViewDto : IDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string SurName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
}
