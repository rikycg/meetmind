namespace MeetMind.Application.Meetings.Common;

public record TranscriptResponse(
    Guid Id,
    Guid MeetingId,
    string Language,
    string Content,
    DateTime CreatedAt
);
