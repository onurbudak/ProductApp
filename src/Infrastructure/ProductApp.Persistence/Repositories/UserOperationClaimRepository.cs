using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class UserOperationClaimRepository : GenericRepositoryAsync<UserOperationClaim, long>, IUserOperationClaimRepository
{
    public UserOperationClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
