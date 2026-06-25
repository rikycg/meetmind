using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class KeyDecisionRepository : IKeyDecisionRepository
{
    private readonly MeetMindDbContext _context;

    public KeyDecisionRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<KeyDecision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.KeyDecisions.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<KeyDecision>> GetAllBySummaryIdAsync(Guid summaryId, CancellationToken cancellationToken = default)
    {
        return await _context.KeyDecisions
            .Where(kd => kd.SummaryId == summaryId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(KeyDecision keyDecision, CancellationToken cancellationToken = default)
    {
        await _context.KeyDecisions.AddAsync(keyDecision, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(KeyDecision keyDecision, CancellationToken cancellationToken = default)
    {
        _context.KeyDecisions.Update(keyDecision);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.KeyDecisions
            .Where(kd => kd.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
