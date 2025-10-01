using ProductApp.Application.Messaging;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.Users.GetAllWithFilterUsers;

public class GetAllWithFilterUsersQuery : IPaginatedQuery<List<UserViewDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }
}
