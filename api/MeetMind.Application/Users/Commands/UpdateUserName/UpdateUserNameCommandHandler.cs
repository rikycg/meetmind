using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Commands.UpdateUserName;

public class UpdateUserNameCommandHandler : IRequestHandler<UpdateUserNameCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserNameCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            throw new KeyNotFoundException($"User with id '{request.Id}' was not found.");

        user.UpdateName(request.FirstName, request.LastName);

        await _userRepository.UpdateAsync(user, cancellationToken);

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
