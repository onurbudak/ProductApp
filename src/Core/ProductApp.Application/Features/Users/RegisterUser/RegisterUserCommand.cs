using ProductApp.Application.Interfaces.Messages;

namespace ProductApp.Application.Features.Users.RegisterUser;

public class RegisterUserCommand : ICommand<bool>
{
    public required string Name { get; set; }
    public required string SurName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public List<long> OperationClaimIds { get; set; } = [];
}