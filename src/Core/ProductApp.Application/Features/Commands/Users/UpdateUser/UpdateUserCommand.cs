using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.Users.UpdateUser;

public class UpdateUserCommand : ICommand<bool>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}
