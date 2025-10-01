using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Commands.Users.RegisterUser;

public class RegisterUserCommand : ICommand<bool>
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}