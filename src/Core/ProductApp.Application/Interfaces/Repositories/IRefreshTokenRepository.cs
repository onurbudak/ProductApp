using ProductApp.Domain.Entities;

namespace ProductApp.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<RefreshToken, long>
{

}
