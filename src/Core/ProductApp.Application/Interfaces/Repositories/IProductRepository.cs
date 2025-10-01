using ProductApp.Domain.Entities;

namespace ProductApp.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepositoryAsync<Product, long>
{

}
