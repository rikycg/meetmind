using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CompleteActionItem;

public record CompleteActionItemCommand(Guid Id) : IRequest<ActionItemResponse>;
