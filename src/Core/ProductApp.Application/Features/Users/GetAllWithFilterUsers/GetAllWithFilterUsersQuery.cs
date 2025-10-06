using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Users.GetAllWithFilterUsers;

public class GetAllWithFilterUsersQuery : IPaginatedQuery<List<UserViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Name { get; set; }
    public string? SurName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
}
