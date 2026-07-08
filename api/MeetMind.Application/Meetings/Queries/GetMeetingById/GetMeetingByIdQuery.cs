using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingById;

public record GetMeetingByIdQuery(Guid Id) : IRequest<MeetingResponse?>;
