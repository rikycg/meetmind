using MediatR;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string Email) : IRequest<UserResponse?>;
