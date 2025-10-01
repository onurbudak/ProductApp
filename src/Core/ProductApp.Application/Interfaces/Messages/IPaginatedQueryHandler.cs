using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Interfaces.Messages;

public interface IPaginatedQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, PaginatedResponse<TResponse>> 
    where TQuery : IPaginatedQuery<TResponse> where TResponse : class, new()
{
}

