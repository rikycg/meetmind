using MediatR;

namespace MeetMind.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;
