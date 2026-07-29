using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Domain.Exceptions;

namespace MeetMind.Application.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken = default)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

        if (storedToken is null)
            throw new NotFoundException("Refresh token not found.");

        if (storedToken.IsRevoked)
            return;

        storedToken.Revoke();
        await _refreshTokenRepository.UpdateAsync(storedToken, cancellationToken);
    }
}
