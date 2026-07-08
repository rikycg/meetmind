using MediatR;

namespace MeetMind.Application.Meetings.Commands.DeleteActionItem;

public record DeleteActionItemCommand(Guid Id) : IRequest;
