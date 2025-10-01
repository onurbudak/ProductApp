using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class UserRepository : GenericRepositoryAsync<User,long>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
