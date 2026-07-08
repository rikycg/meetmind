using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CancelActionItem;

public record CancelActionItemCommand(Guid Id) : IRequest<ActionItemResponse>;
