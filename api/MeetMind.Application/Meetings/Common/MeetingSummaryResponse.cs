namespace MeetMind.Application.Meetings.Common;

public record MeetingSummaryResponse(
    Guid Id,
    Guid MeetingId,
    string Summary,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
