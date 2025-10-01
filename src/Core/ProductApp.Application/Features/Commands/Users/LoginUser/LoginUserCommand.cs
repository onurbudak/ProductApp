using ProductApp.Application.Messaging;
using ProductApp.Application.Services;

namespace ProductApp.Application.Features.Commands.Users.LoginUser;

public class LoginUserCommand : ICommand<AccessToken>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}