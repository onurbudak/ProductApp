using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Interfaces.Messages;

public interface ICommand : IRequest<BaseResponse>
{
}

public interface ICommand<TResponse> : IRequest<ServiceResponse<TResponse>>
{
}
