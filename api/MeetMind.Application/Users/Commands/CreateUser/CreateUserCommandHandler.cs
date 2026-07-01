using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Users.Common;
using MeetMind.Domain.Users;

namespace MeetMind.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.ExistsAsync(request.Email, cancellationToken);

        if (exists)
            throw new InvalidOperationException($"A user with email '{request.Email}' already exists.");

        var user = User.Create(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password
        );

        await _userRepository.AddAsync(user, cancellationToken);

        return new UserResponse(
            user.Id,
            user.Email.Value,
            user.FullName.FirstName,
            user.FullName.LastName,
            user.Role.ToString(),
            user.CreatedAt,
            user.UpdatedAt
        );
    }
}
