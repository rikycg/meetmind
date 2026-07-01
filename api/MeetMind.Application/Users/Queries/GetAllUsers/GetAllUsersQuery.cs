using MediatR;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserResponse>>;
