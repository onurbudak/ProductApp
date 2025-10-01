using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Interfaces.Messages;

public interface IPaginatedQuery<TResponse> : IRequest<PaginatedResponse<TResponse>>, IPaginatedQuery where TResponse : class, new()
{
}

public interface IPaginatedQuery
{
    int PageSize { get; set; }

    int PageNumber { get; set; }
}
