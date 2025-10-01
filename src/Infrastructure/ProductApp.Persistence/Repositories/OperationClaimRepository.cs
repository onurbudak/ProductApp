using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class OperationClaimRepository : GenericRepositoryAsync<OperationClaim, long>, IOperationClaimRepository
{
    public OperationClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
