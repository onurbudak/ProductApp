using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UserOperationClaims.CreateUserOperationClaim;

public class CreateUserOperationClaimCommandHandler : ICommandHandler<CreateUserOperationClaimCommand, bool>
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
        _ = await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);
        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}
