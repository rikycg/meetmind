using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingsByTeamId;

public record GetMeetingsByTeamIdQuery(Guid TeamId) : IRequest<IEnumerable<MeetingResponse>>;
