using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.Users.DeleteUser;

public class DeleteUserCommand : ICommand<bool>
{
    public long Id { get; set; }
}
