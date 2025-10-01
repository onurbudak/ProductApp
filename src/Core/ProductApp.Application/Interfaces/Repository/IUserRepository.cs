using ProductApp.Domain.Entities;

namespace ProductApp.Application.Interfaces.Repository;

public interface IUserRepository : IGenericRepositoryAsync<User,long>
{

}
