using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Extensions;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.UserOperationClaims.GetAllUserOperationClaims;

public class GetAllUserOperationClaimsQueryHandler : IPaginatedQueryHandler<GetAllUserOperationClaimsQuery, List<UserOperationClaimViewDto>>
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public GetAllUserOperationClaimsQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<List<UserOperationClaimViewDto>>> Handle(GetAllUserOperationClaimsQuery request, CancellationToken cancellationToken)
    {
        List<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetAllAsync();

        if (userOperationClaims.Count == 0)
        {
            return PaginatedResponse<List<UserOperationClaimViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        userOperationClaims.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<UserOperationClaimViewDto> productViewDtos = _mapper.Map<List<UserOperationClaimViewDto>>(paginatedDatas);

        return PaginatedResponse<List<UserOperationClaimViewDto>>.SuccessPaginatedDataWithMessage(productViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);

    }
}

