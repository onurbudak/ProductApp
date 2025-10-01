using ProductApp.Application.Helpers;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userRepository = userRepository;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<ServiceResponse<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        List<User> users = await _userRepository.GetAllWithFilterAsync(e => e.UserName == request.UserName && e.Email == request.Email);

        if (users.Any())
            throw new InvalidOperationException("Username already exists");

        var user = new User
        {
            UserName = request.UserName,
            Name = request.Name,
            SurName = request.SurName,
            Email = request.Email
        };
        HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        User addedUser = await _userRepository.AddAsync(user);
        UserOperationClaim userOperationClaim = new UserOperationClaim { OperationClaimId = 1, UserId = addedUser.Id };
        UserOperationClaim addedUserOperationClaim = await _userOperationClaimRepository.AddAsync(userOperationClaim);

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}