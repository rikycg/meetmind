using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CompleteMeeting;

public record CompleteMeetingCommand(Guid Id) : IRequest<MeetingResponse>;
