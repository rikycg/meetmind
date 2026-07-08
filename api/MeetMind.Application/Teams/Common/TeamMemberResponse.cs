namespace MeetMind.Application.Teams.Common;

public record TeamMemberResponse(
    Guid Id,
    Guid UserId,
    Guid TeamId,
    string Role,
    DateTime JoinedAt
);
