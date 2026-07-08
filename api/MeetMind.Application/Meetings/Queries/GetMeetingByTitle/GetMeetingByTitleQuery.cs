using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingByTitle;

public record GetMeetingByTitleQuery(string Title) : IRequest<MeetingResponse?>;
