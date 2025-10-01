using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Interfaces.Messages;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ServiceResponse<TResponse>> where TQuery : IQuery<TResponse>
{
}
