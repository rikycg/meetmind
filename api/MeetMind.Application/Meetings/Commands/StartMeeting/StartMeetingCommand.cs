using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.StartMeeting;

public record StartMeetingCommand(Guid Id) : IRequest<MeetingResponse>;
