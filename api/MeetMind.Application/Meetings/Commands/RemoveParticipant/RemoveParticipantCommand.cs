using MediatR;

namespace MeetMind.Application.Meetings.Commands.RemoveParticipant;

public record RemoveParticipantCommand(Guid Id) : IRequest;
