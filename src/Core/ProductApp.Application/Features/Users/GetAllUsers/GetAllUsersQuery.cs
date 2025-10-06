using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Users.GetAllUsers;

public class GetAllUsersQuery : IPaginatedQuery<List<UserViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
