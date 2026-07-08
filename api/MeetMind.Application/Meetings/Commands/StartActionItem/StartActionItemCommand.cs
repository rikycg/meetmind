using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.StartActionItem;

public record StartActionItemCommand(Guid Id) : IRequest<ActionItemResponse>;
