using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.Users.DeleteUser;

public class DeleteOperationClaimCommandHandler : ICommandHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteOperationClaimCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User mappedUser = _mapper.Map<User>(request);
        User? user = await _userRepository.DeleteAsync(mappedUser);

        if (user is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

