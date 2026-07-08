using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetAllMeetings;

public record GetAllMeetingsQuery() : IRequest<IEnumerable<MeetingResponse>>;
