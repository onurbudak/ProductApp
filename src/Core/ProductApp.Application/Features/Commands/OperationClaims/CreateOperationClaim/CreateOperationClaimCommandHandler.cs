using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.OperationClaims.CreateOperationClaim;

public class CreateOperationClaimCommandHandler : ICommandHandler<CreateOperationClaimCommand, bool>
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;

    public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
        _ = await _operationClaimRepository.AddAsync(mappedOperationClaim);
        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

