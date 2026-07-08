using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetParticipantsByMeetingId;

public record GetParticipantsByMeetingIdQuery(Guid MeetingId) : IRequest<IEnumerable<ParticipantResponse>>;
