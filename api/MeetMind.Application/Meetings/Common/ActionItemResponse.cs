namespace MeetMind.Application.Meetings.Common;

public record ActionItemResponse(
    Guid Id,
    Guid SummaryId,
    Guid? AssignedTo,
    string Title,
    string? Description,
    DateTime? DueDate,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
