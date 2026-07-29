using MediatR;
using MeetMind.Application.Auth.Common;

namespace MeetMind.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
