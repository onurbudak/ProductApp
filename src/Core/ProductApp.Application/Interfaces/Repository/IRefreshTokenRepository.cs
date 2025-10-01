using ProductApp.Domain.Entities;

namespace ProductApp.Application.Interfaces.Repository;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<RefreshToken, long>
{

}
