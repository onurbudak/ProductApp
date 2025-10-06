using MediatR;
using ProductApp.Application.Common;
using ProductApp.Application.Exceptions;
using ProductApp.Application.Features.Users.CreateUser;
using ProductApp.Application.Features.Users.GetAllWithFilterUsers;
using ProductApp.Application.Helpers;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, bool>
{
    private readonly IMediator _mediator;

    public RegisterUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        PaginatedResponse<List<UserViewDto>> paginatedResponse = await _mediator.Send(new GetAllWithFilterUsersQuery() { UserName = request.UserName }, cancellationToken);

        if (paginatedResponse.Data is not null && paginatedResponse.Data.Count != 0)
        {
            throw new UnauthorizedException(Messages.UsernameAlreadyExist);
        }

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

        ServiceResponse<User> serviceResponseUser = await _mediator.Send(new CreateUserCommand() { UserName = user.UserName, Email = user.Email, Name = user.Name, SurName = user.SurName, PasswordHash = user.PasswordHash, PasswordSalt = user.PasswordSalt }, cancellationToken);
        if (!serviceResponseUser.IsSuccess)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.Error, new Error(MessageCode.Error, Messages.Error));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}