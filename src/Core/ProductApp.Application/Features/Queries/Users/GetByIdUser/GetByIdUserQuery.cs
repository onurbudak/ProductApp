using ProductApp.Application.Messaging;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Queries.Users.GetByIdUser;

public class GetByIdUserQuery : IQuery<UserViewDto>
{
    public long Id { get; set; }
}
