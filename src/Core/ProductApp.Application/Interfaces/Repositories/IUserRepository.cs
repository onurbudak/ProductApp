using ProductApp.Domain.Entities;

namespace ProductApp.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepositoryAsync<User,long>
{

}
