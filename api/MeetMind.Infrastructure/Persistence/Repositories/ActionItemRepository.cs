using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class ActionItemRepository : IActionItemRepository {
    private readonly MeetMindDbContext _context;

    public ActionItemRepository(MeetMindDbContext context)
    {   
        _context = context;
    }

    public async Task<ActionItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ActionItems.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<ActionItem>> GetAllBySummaryIdAsync(Guid summaryId, CancellationToken cancellationToken = default)
    {
        return await _context.ActionItems
            .Where(a => a.SummaryId == summaryId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ActionItem>> GetAllByAssignedToAsync(Guid assignedTo, CancellationToken cancellationToken = default)
    {
        return await _context.ActionItems
            .Where(a => a.AssignedTo == assignedTo)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ActionItem>> GetAllByDueDateAsync(DateTime dueDate, CancellationToken cancellationToken = default)
    {
        return await _context.ActionItems
            .Where(a => a.DueDate < dueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ActionItem>> GetAllByStatusAsync(ActionItemStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.ActionItems
            .Where(a => a.Status == status)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ActionItem actionItem, CancellationToken cancellationToken = default)
    {
        await _context.ActionItems.AddAsync(actionItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ActionItem actionItem, CancellationToken cancellationToken = default)
    {
        _context.ActionItems.Update(actionItem);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.ActionItems
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
