using ProductApp.Application.Interfaces.Messages;
using ProductApp.Domain.Dto;

namespace ProductApp.Application.Features.Users.GetByIdUser;

public class GetByIdUserQuery : IQuery<UserViewDto>
{
    public long Id { get; set; }
}
