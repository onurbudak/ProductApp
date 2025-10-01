using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class RefreshTokenRepository : GenericRepositoryAsync<RefreshToken, long>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
