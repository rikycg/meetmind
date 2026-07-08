using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.StartActionItem;

public class StartActionItemCommandHandler : IRequestHandler<StartActionItemCommand, ActionItemResponse>
{
    private readonly IActionItemRepository _actionItemRepository;

    public StartActionItemCommandHandler(IActionItemRepository actionItemRepository)
    {
        _actionItemRepository = actionItemRepository;
    }

    public async Task<ActionItemResponse> Handle(StartActionItemCommand request, CancellationToken cancellationToken = default)
    {
        var actionItem = await _actionItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (actionItem is null)
            throw new KeyNotFoundException($"Action item with id '{request.Id}' was not found.");

        actionItem.Start();

        await _actionItemRepository.UpdateAsync(actionItem, cancellationToken);

        return new ActionItemResponse(
            actionItem.Id,
            actionItem.SummaryId,
            actionItem.AssignedTo,
            actionItem.Title,
            actionItem.Description,
            actionItem.DueDate,
            actionItem.Status.ToString(),
            actionItem.CreatedAt,
            actionItem.UpdatedAt
        );
    }
}
