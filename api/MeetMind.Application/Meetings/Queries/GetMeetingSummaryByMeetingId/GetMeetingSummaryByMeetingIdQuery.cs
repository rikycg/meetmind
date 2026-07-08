using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingSummaryByMeetingId;

public record GetMeetingSummaryByMeetingIdQuery(Guid MeetingId) : IRequest<MeetingSummaryResponse?>;
