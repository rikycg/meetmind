using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetKeyDecisionsBySummaryId;

public record GetKeyDecisionsBySummaryIdQuery(Guid SummaryId) : IRequest<IEnumerable<KeyDecisionResponse>>;
