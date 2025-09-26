using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Messaging;

public interface IQuery<TResponse> : IRequest<ServiceResponse<TResponse>>
{
}
