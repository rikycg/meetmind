using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.AddParticipant;

public record AddParticipantCommand(Guid UserId, Guid MeetingId, string Role) : IRequest<ParticipantResponse>;
