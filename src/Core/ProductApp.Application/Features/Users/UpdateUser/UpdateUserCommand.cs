using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Users.UpdateUser;

public class UpdateUserCommand : ICommand<User>
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string SurName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
}
