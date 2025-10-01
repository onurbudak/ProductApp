using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.OperationClaims.UpdateOperationClaim;

public class UpdateOperationClaimCommandHandler : ICommandHandler<UpdateOperationClaimCommand, bool>
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOperationClaimCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, ILogger<UpdateOperationClaimCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<ServiceResponse<bool>> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
        OperationClaim? operationClaim = await _operationClaimRepository.UpdateAsync(mappedOperationClaim);

        if (operationClaim is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

