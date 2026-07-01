using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserResponse?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return null;

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
