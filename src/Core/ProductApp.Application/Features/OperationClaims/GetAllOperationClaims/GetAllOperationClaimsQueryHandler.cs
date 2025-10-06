using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Extensions;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.OperationClaims.GetAllOperationClaims;

internal class GetAllOperationClaimsQueryHandler : IPaginatedQueryHandler<GetAllOperationClaimsQuery, List<OperationClaimViewDto>>
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;

    public GetAllOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<List<OperationClaimViewDto>>> Handle(GetAllOperationClaimsQuery request, CancellationToken cancellationToken)
    {
        List<OperationClaim> operationClaims = await _operationClaimRepository.GetAllAsync();

        if (operationClaims.Count == 0)
        {
            return PaginatedResponse<List<OperationClaimViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        operationClaims.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<OperationClaimViewDto> operationClaimViewDtos = _mapper.Map<List<OperationClaimViewDto>>(paginatedDatas);

        return PaginatedResponse<List<OperationClaimViewDto>>.SuccessPaginatedDataWithMessage(operationClaimViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);

    }
}


