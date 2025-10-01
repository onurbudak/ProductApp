using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Interfaces.Messages;

public interface IQuery<TResponse> : IRequest<ServiceResponse<TResponse>>
{
}
