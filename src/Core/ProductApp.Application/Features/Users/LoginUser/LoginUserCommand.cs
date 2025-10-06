using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Users.LoginUser;

public class LoginUserCommand : ICommand<AccessToken>
{
    public required string UserName { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }

}