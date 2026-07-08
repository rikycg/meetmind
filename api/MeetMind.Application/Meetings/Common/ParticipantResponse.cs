namespace MeetMind.Application.Meetings.Common;

public record ParticipantResponse(
    Guid Id,
    Guid UserId,
    Guid MeetingId,
    string Role,
    DateTime JoinedAt,
    DateTime? LeftAt
);
