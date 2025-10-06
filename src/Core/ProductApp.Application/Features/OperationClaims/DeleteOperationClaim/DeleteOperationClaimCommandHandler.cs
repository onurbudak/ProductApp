using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.OperationClaims.DeleteOperationClaim;

public class DeleteOperationClaimCommandHandler : ICommandHandler<DeleteOperationClaimCommand, bool>
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;

    public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
    {
        OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
        OperationClaim? operationClaim = await _operationClaimRepository.DeleteAsync(mappedOperationClaim);

        if (operationClaim is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

