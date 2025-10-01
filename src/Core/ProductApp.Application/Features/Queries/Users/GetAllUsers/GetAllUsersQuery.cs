using ProductApp.Application.Messaging;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.Users.GetAllUsers;

public class GetAllUsersQuery : IPaginatedQuery<List<UserViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
