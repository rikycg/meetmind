using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Commands.CreateActionItem;

public class CreateActionItemCommandHandler : IRequestHandler<CreateActionItemCommand, ActionItemResponse>
{
    private readonly IActionItemRepository _actionItemRepository;

    public CreateActionItemCommandHandler(IActionItemRepository actionItemRepository)
    {
        _actionItemRepository = actionItemRepository;
    }

    public async Task<ActionItemResponse> Handle(CreateActionItemCommand request, CancellationToken cancellationToken = default)
    {
        var actionItem = ActionItem.Create(
            request.SummaryId,
            request.Title,
            request.Description,
            request.AssignedTo,
            request.DueDate
        );

        await _actionItemRepository.AddAsync(actionItem, cancellationToken);

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
