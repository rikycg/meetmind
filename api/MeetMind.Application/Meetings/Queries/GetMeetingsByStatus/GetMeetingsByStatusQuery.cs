using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingsByStatus;

public record GetMeetingsByStatusQuery(string Status) : IRequest<IEnumerable<MeetingResponse>>;
