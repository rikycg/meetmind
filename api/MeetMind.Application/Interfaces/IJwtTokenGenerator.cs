using MeetMind.Domain.Users;

namespace MeetMind.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    (string Token, DateTime ExpiresAt) GenerateRefreshToken();
}
