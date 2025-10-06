using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.UserOperationClaims.GetByIdUserOperationClaim;

public class GetByIdUserOperationClaimQueryHandler : IQueryHandler<GetByIdUserOperationClaimQuery, UserOperationClaimViewDto>
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public GetByIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<UserOperationClaimViewDto>> Handle(GetByIdUserOperationClaimQuery request, CancellationToken cancellationToken)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetByIdAsync(request.Id);

        if (userOperationClaim is null)
        {
            return ServiceResponse<UserOperationClaimViewDto>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }
        UserOperationClaimViewDto userOperationClaimViewDto = _mapper.Map<UserOperationClaimViewDto>(userOperationClaim);

        return ServiceResponse<UserOperationClaimViewDto>.SuccessDataWithMessage(userOperationClaimViewDto, Messages.Success);
    }
}
