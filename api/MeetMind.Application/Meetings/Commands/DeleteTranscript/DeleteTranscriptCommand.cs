using MediatR;

namespace MeetMind.Application.Meetings.Commands.DeleteTranscript;

public record DeleteTranscriptCommand(Guid Id) : IRequest;
