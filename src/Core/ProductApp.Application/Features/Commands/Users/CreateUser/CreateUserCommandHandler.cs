using AutoMapper;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    } 
    public async Task<ServiceResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User mappedUser = _mapper.Map<User>(request);
        _ = await _userRepository.AddAsync(mappedUser);
        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

