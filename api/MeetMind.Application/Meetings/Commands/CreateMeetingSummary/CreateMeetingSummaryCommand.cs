using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CreateMeetingSummary;

public record CreateMeetingSummaryCommand(Guid MeetingId, string Summary) : IRequest<MeetingSummaryResponse>;
