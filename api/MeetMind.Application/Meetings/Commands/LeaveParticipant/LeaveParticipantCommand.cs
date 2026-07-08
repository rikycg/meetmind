using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.LeaveParticipant;

public record LeaveParticipantCommand(Guid Id) : IRequest<ParticipantResponse>;
