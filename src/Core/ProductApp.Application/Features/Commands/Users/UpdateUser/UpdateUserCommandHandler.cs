using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.Users.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<UpdateUserCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<ServiceResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User mappedUser = _mapper.Map<User>(request);
        User? user = await _userRepository.UpdateAsync(mappedUser);

        if (user is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

