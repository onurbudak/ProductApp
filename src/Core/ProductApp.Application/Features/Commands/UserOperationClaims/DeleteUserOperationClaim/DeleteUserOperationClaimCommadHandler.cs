using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UserOperationClaims.DeleteUserOperationClaim;

public class DeleteUserOperationClaimCommandHandler : ICommandHandler<DeleteUserOperationClaimCommand, bool>
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<bool>> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
        UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.DeleteAsync(mappedUserOperationClaim);

        if (userOperationClaim is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

