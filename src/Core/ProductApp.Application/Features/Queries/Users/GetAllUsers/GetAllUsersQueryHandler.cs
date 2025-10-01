using AutoMapper;
using ProductApp.Application.Extensions;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.Users.GetAllUsers;

public class GetAllUsersQueryHandler : IPaginatedQueryHandler<GetAllUsersQuery, List<UserViewDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<List<UserViewDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        List<User> Users = await _userRepository.GetAllAsync();

        if (Users.Count == 0)
        {
            return PaginatedResponse<List<UserViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        Users.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<UserViewDto> UserViewDtos = _mapper.Map<List<UserViewDto>>(paginatedDatas);

        return PaginatedResponse<List<UserViewDto>>.SuccessPaginatedDataWithMessage(UserViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);

    }
}

