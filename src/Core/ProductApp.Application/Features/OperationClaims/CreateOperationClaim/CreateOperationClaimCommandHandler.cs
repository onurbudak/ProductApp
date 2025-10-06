using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.OperationClaims.CreateOperationClaim;

public class CreateOperationClaimCommandHandler : ICommandHandler<CreateOperationClaimCommand, OperationClaim>
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;

    public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<OperationClaim>> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
        OperationClaim operationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);
        return ServiceResponse<OperationClaim>.SuccessDataWithMessage(operationClaim, Messages.Success);
    }
}

