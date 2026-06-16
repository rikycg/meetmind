using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface IActionItemRepository {
    Task<ActionItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ActionItem>> GetAllBySummaryIdAsync(Guid summaryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ActionItem>> GetAllByAssignedToAsync(Guid assignedTo, CancellationToken cancellationToken = default);
    Task<IEnumerable<ActionItem>> GetAllByDueDateAsync(DateTime dueDate, CancellationToken cancellationToken = default);
    Task<IEnumerable<ActionItem>> GetAllByStatusAsync(ActionItemStatus status, CancellationToken cancellationToken = default);
    Task AddAsync(ActionItem actionItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(ActionItem actionItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
