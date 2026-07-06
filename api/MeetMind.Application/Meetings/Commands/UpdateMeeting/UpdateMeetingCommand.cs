using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.UpdateMeeting;

public record UpdateMeetingCommand(
    Guid Id,
    string Title,
    string Description
) : IRequest<MeetingResponse>;
