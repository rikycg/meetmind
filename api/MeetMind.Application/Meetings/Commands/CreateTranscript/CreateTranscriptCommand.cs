using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CreateTranscript;

public record CreateTranscriptCommand(Guid MeetingId, string Language, string Content) : IRequest<TranscriptResponse>;
