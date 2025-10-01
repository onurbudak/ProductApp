using AutoMapper;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.Users.GetByIdUser;

public class GetByIdUserQueryHandler : IQueryHandler<GetByIdUserQuery, UserViewDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<UserViewDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        User? product = await _userRepository.GetByIdAsync(request.Id);

        if (product is null)
        {
            return ServiceResponse<UserViewDto>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }
        UserViewDto productViewDto = _mapper.Map<UserViewDto>(product);

        return ServiceResponse<UserViewDto>.SuccessDataWithMessage(productViewDto, Messages.Success);
    }
}

