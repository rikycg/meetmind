using MediatR;
using MeetMind.Application.Users.Common;

namespace MeetMind.Application.Users.Commands.UpdateUserName;

public record UpdateUserNameCommand(
    Guid Id,
    string FirstName,
    string LastName
) : IRequest<UserResponse>;
