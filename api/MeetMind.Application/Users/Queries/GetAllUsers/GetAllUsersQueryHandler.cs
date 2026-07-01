using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        return users.Select(user => new UserResponse(
            user.Id,
            user.Email.Value,
            user.FullName.FirstName,
            user.FullName.LastName,
            user.Role.ToString(),
            user.CreatedAt,
            user.UpdatedAt
        ));
    }
}
