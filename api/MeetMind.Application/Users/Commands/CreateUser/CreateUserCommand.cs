using MediatR;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password
) : IRequest<UserResponse>;
