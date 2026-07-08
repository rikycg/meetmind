using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetAudioRecordingsByMeetingId;

public record GetAudioRecordingsByMeetingIdQuery(Guid MeetingId) : IRequest<IEnumerable<AudioRecordingResponse>>;
