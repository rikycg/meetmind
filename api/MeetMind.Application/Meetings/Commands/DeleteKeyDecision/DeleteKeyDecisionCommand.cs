using MediatR;

namespace MeetMind.Application.Meetings.Commands.DeleteKeyDecision;

public record DeleteKeyDecisionCommand(Guid Id) : IRequest;
