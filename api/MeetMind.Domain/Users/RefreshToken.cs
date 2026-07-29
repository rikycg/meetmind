using MeetMind.Domain.Common;
using MeetMind.Domain.Exceptions;

namespace MeetMind.Domain.Users;

public sealed class RefreshToken : Entity
{
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt is not null;
    public bool IsActive => !IsExpired && !IsRevoked;

    private RefreshToken() : base() { }

    private RefreshToken(string token, Guid userId, DateTime expiresAt) : base()
    {
        Token = token;
        UserId = userId;
        ExpiresAt = expiresAt;
    }

    public static RefreshToken Create(string token, Guid userId, DateTime expiresAt)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("token is empty.");

        if (userId == Guid.Empty)
            throw new ArgumentException("userId is empty.");

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("expiresAt must be in the future.");

        return new RefreshToken(token, userId, expiresAt);
    }

    public void Revoke()
    {
        if (IsRevoked)
            throw new ConflictException("Refresh token is already revoked.");

        RevokedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
