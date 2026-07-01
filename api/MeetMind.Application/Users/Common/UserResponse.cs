namespace MeetMind.Application.Users.Common;

public record UserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Role,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
