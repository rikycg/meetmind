using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CancelMeeting;

public record CancelMeetingCommand(
    Guid Id
): IRequest<MeetingResponse>;
