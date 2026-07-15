using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Domain.Exceptions;

namespace MeetMind.Application.Meetings.Commands.DeleteActionItem;

public class DeleteActionItemCommandHandler : IRequestHandler<DeleteActionItemCommand>
{
    private readonly IActionItemRepository _actionItemRepository;

    public DeleteActionItemCommandHandler(IActionItemRepository actionItemRepository)
    {
        _actionItemRepository = actionItemRepository;
    }

    public async Task Handle(DeleteActionItemCommand request, CancellationToken cancellationToken = default)
    {
        var actionItem = await _actionItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (actionItem is null)
            throw new NotFoundException($"Action item with id '{request.Id}' was not found.");

        await _actionItemRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
