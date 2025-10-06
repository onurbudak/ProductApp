using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.UserOperationClaims.CreateUserOperationClaim;

public class CreateUserOperationClaimCommandHandler : ICommandHandler<CreateUserOperationClaimCommand, UserOperationClaim>
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<UserOperationClaim>> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        if (request.OperationClaimIds.Count == 0)
        {
            return ServiceResponse<UserOperationClaim>.FailureDataWithMessage(Messages.Error, new Error(MessageCode.Error, Messages.Error));
        }

        UserOperationClaim? userOperationClaim = null;
        UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
        foreach (long operationClaimId in request.OperationClaimIds)
        {       
            mappedUserOperationClaim.OperationClaimId = operationClaimId;   
            userOperationClaim = await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);
        }
        return ServiceResponse<UserOperationClaim>.SuccessDataWithMessage(userOperationClaim, Messages.Success);
    }
}
