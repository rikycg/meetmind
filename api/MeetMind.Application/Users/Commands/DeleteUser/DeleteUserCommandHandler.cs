using MediatR;
using MeetMind.Application.Interfaces;

namespace MeetMind.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            throw new KeyNotFoundException($"User with id '{request.Id}' was not found.");

        await _userRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
