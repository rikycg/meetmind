namespace MeetMind.Application.Teams.Common;

public record TeamResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
