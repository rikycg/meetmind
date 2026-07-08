namespace MeetMind.Application.Meetings.Common;

public record KeyDecisionResponse(
    Guid Id,
    Guid SummaryId,
    string Content,
    DateTime CreatedAt
);
