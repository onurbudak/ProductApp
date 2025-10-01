using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Interfaces.Messages;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, BaseResponse> where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, ServiceResponse<TResponse>> where TCommand : ICommand<TResponse>
{
}
