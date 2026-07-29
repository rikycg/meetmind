using MediatR;

namespace MeetMind.Application.Auth.Commands.Logout;

public record LogoutCommand(string RefreshToken) : IRequest;
