using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CreateActionItem;

public record CreateActionItemCommand(
    Guid SummaryId,
    string Title,
    string? Description,
    Guid? AssignedTo,
    DateTime? DueDate
) : IRequest<ActionItemResponse>;
