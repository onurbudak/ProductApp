using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class OperationClaimRepository : GenericRepositoryAsync<OperationClaim>, IOperationClaimRepository
{
    public OperationClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
