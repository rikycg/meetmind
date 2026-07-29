using MediatR;
using MeetMind.Application.Auth.Common;

namespace MeetMind.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponse>;
