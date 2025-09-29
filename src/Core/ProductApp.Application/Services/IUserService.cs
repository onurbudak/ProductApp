using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Services;

public interface IUserService
{
    Task<ServiceResponse<AccessToken>> Login(LoginUserCommand request, CancellationToken cancellationToken = default);

    Task<ServiceResponse<bool>> Register(RegisterUserCommand request, CancellationToken cancellationToken = default);

}