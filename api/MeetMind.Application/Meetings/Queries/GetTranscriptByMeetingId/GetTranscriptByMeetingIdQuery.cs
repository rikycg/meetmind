using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetTranscriptByMeetingId;

public record GetTranscriptByMeetingIdQuery(Guid MeetingId) : IRequest<TranscriptResponse?>;
