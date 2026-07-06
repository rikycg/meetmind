namespace MeetMind.Application.Meetings.Common;

public record MeetingResponse (
    Guid Id,
    string Title,
    string Description,
    Guid HostId,
    Guid? TeamId,
    DateTime ScheduledAt,
    DateTime? StartedAt,
    DateTime? EndedAt,
    string Status
);
