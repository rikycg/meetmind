using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CreateKeyDecision;

public record CreateKeyDecisionCommand(Guid SummaryId, string Content) : IRequest<KeyDecisionResponse>;
