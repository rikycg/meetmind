using MediatR;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserResponse?>;
