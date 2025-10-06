using System.Text.Json.Serialization;
using ProductApp.Domain.Common;

namespace ProductApp.Domain.Dto;

public class UserViewDto : IDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string SurName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = [];
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = [];
}
