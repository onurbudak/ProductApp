using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UserOperationClaims.UpdateUserOperationClaim;

public class UpdateUserOperationClaimCommandHandler : ICommandHandler<UpdateUserOperationClaimCommand, bool>
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateUserOperationClaimCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, ILogger<UpdateUserOperationClaimCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _mapper = mapper;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<ServiceResponse<bool>> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
        UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.UpdateAsync(mappedUserOperationClaim);

        if (userOperationClaim is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

