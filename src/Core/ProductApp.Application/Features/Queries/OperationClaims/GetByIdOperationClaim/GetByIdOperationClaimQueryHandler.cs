using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.OperationClaims.GetByIdOperationClaim;

public class GetByIdOperationClaimQueryHandler : IQueryHandler<GetByIdOperationClaimQuery, OperationClaimViewDto>
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;

    public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<OperationClaimViewDto>> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
    {
        OperationClaim? operationClaim = await _operationClaimRepository.GetByIdAsync(request.Id);

        if (operationClaim is null)
        {
            return ServiceResponse<OperationClaimViewDto>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }
        OperationClaimViewDto operationClaimViewDto = _mapper.Map<OperationClaimViewDto>(operationClaim);

        return ServiceResponse<OperationClaimViewDto>.SuccessDataWithMessage(operationClaimViewDto, Messages.Success);
    }
}


