namespace MeetMind.Application.Auth.Common;

public record AuthResponse(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    string Role,
    string AccessToken,
    string RefreshToken
);
