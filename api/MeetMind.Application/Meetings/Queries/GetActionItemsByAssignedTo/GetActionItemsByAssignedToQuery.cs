using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetActionItemsByAssignedTo;

public record GetActionItemsByAssignedToQuery(Guid UserId) : IRequest<IEnumerable<ActionItemResponse>>;
