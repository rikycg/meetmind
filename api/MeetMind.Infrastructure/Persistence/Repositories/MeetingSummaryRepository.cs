using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class MeetingSummaryRepository : IMeetingSummaryRepository
{
    private readonly MeetMindDbContext _context;

    public MeetingSummaryRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<MeetingSummary?> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default)
    {
        return await _context.MeetingSummaries
            .FirstOrDefaultAsync(ms => ms.MeetingId == meetingId, cancellationToken);
    }

    public async Task<IEnumerable<MeetingSummary>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MeetingSummaries.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MeetingSummary meetingSummary, CancellationToken cancellationToken = default)
    {
        await _context.MeetingSummaries.AddAsync(meetingSummary, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MeetingSummary meetingSummary, CancellationToken cancellationToken = default)
    {
        _context.MeetingSummaries.Update(meetingSummary);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.MeetingSummaries
            .Where(ms => ms.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
