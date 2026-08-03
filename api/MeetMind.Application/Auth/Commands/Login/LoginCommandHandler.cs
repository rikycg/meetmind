using MediatR;
using MeetMind.Application.Auth.Common;
using MeetMind.Application.Interfaces;
using MeetMind.Domain.Exceptions;
using RefreshTokenEntity = MeetMind.Domain.Users.RefreshToken;

namespace MeetMind.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new BadRequestException("Invalid credentials.");

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var (refreshTokenValue, expiresAt) = _jwtTokenGenerator.GenerateRefreshToken();

        var refreshToken = RefreshTokenEntity.Create(refreshTokenValue, user.Id, expiresAt);
        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

        return new AuthResponse(
            user.Id,
            user.Email.Value,
            user.FullName.FirstName,
            user.FullName.LastName,
            user.Role.ToString(),
            accessToken,
            refreshToken.Token
        );
    }
}
