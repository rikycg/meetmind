namespace MeetMind.Application.Meetings.Common;

public record AudioRecordingResponse(
    Guid Id,
    Guid MeetingId,
    string FileUrl,
    int Duration,
    long FileSize,
    string Format,
    DateTime CreatedAt
);
