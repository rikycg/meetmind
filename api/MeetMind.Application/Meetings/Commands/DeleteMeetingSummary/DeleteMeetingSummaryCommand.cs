using MediatR;

namespace MeetMind.Application.Meetings.Commands.DeleteMeetingSummary;

public record DeleteMeetingSummaryCommand(Guid Id) : IRequest;
