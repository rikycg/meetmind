using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetActionItemsByAssignedTo;

public class GetActionItemsByAssignedToQueryHandler : IRequestHandler<GetActionItemsByAssignedToQuery, IEnumerable<ActionItemResponse>>
{
    private readonly IActionItemRepository _actionItemRepository;

    public GetActionItemsByAssignedToQueryHandler(IActionItemRepository actionItemRepository)
    {
        _actionItemRepository = actionItemRepository;
    }

    public async Task<IEnumerable<ActionItemResponse>> Handle(GetActionItemsByAssignedToQuery request, CancellationToken cancellationToken = default)
    {
        var items = await _actionItemRepository.GetAllByAssignedToAsync(request.UserId, cancellationToken);

        return items.Select(item => new ActionItemResponse(
            item.Id,
            item.SummaryId,
            item.AssignedTo,
            item.Title,
            item.Description,
            item.DueDate,
            item.Status.ToString(),
            item.CreatedAt,
            item.UpdatedAt
        ));
    }
}
