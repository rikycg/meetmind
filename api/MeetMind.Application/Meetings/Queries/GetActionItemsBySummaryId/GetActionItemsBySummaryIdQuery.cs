using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetActionItemsBySummaryId;

public record GetActionItemsBySummaryIdQuery(Guid SummaryId) : IRequest<IEnumerable<ActionItemResponse>>;
