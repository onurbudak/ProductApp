using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Users.CreateUser;

public class CreateUserCommand : ICommand<User>
{
    public required string Name { get; set; }
    public required string SurName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
}
