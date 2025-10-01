using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class ProductRepository : GenericRepositoryAsync<Product, long>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
