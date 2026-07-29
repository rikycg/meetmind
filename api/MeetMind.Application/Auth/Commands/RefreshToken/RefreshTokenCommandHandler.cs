using MediatR;
using MeetMind.Application.Auth.Common;
using MeetMind.Application.Interfaces;
using MeetMind.Domain.Exceptions;
using RefreshTokenEntity = MeetMind.Domain.Users.RefreshToken;

namespace MeetMind.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken = default)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

        if (storedToken is null || !storedToken.IsActive)
            throw new BadRequestException("Invalid or expired refresh token.");

        var user = await _userRepository.GetByIdAsync(storedToken.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found.");

        storedToken.Revoke();
        await _refreshTokenRepository.UpdateAsync(storedToken, cancellationToken);

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var (newTokenValue, expiresAt) = _jwtTokenGenerator.GenerateRefreshToken();

        var newRefreshToken = RefreshTokenEntity.Create(newTokenValue, user.Id, expiresAt);
        await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);

        return new AuthResponse(
            user.Id,
            user.Email.Value,
            user.FullName.FirstName,
            user.FullName.LastName,
            user.Role.ToString(),
            accessToken,
            newRefreshToken.Token
        );
    }
}
