using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Extensions;
using ProductApp.Application.Filters;
using ProductApp.Application.Interfaces.Filters;
using ProductApp.Application.Interfaces.Messages;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Dto;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Users.GetAllWithFilterUsers;

public class GetAllWithFilterUsersQueryHandler : IPaginatedQueryHandler<GetAllWithFilterUsersQuery, List<UserViewDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IFilterService<User> _filterService;

    public GetAllWithFilterUsersQueryHandler(IUserRepository userRepository, IMapper mapper, IFilterService<User> filterService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _filterService = filterService;
    }

    public async Task<PaginatedResponse<List<UserViewDto>>> Handle(GetAllWithFilterUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository.Query();

        List<FilterCriteria> filters = new List<FilterCriteria>();

        if (!string.IsNullOrWhiteSpace(request.Name))
            filters.Add(new FilterCriteria { Field = "Name", Operator = "==", Value = request.Name });

        if (!string.IsNullOrWhiteSpace(request.SurName))
            filters.Add(new FilterCriteria { Field = "SurName", Operator = "==", Value = request.SurName });

        if (!string.IsNullOrWhiteSpace(request.Email))
            filters.Add(new FilterCriteria { Field = "Email", Operator = "==", Value = request.Email });

        if (!string.IsNullOrWhiteSpace(request.UserName))
            filters.Add(new FilterCriteria { Field = "UserName", Operator = "==", Value = request.UserName });

        query = _filterService.ApplyFilters(query, filters);

        List<User> users = query.ToList();

        if (users.Count == 0)
        {
            return PaginatedResponse<List<UserViewDto>>.FailurePaginatedDataWithMessage(Messages.RecordIsNotFound,
                new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        users.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
        List<UserViewDto> productViewDtos = _mapper.Map<List<UserViewDto>>(paginatedDatas);

        return PaginatedResponse<List<UserViewDto>>.SuccessPaginatedDataWithMessage(
            productViewDtos, Messages.Success, totalItems, request.PageNumber, request.PageSize);
    }
}


