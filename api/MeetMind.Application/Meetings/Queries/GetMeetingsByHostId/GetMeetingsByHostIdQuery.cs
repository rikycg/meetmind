using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingsByHostId;

public record GetMeetingsByHostIdQuery(Guid HostId) : IRequest<IEnumerable<MeetingResponse>>;
