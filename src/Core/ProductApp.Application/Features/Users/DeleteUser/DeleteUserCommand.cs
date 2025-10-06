using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Users.DeleteUser;

public class DeleteUserCommand : ICommand<bool>
{
    public long Id { get; set; }
}
