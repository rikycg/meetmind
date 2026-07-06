using MediatR;

namespace MeetMind.Application.Meetings.Commands.DeleteMeeting;

public record DeleteMeetingCommand(Guid Id) : IRequest;
