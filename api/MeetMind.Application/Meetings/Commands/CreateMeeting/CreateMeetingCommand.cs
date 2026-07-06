using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CreateMeeting;

public record CreateMeetingCommand(
    string Title,
    string Description,
    Guid HostId,
    Guid? TeamId,
    DateTime ScheduledAt
) : IRequest<MeetingResponse>;
