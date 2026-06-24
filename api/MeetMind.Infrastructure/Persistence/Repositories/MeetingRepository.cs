using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class MeetingRepository : IMeetingRepository {
    private readonly MeetMindDbContext _context;

    public MeetingRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<Meeting?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Meetings.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Meeting?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Meetings.FirstOrDefaultAsync(m => m.Title.Contains(title), cancellationToken);
    }

    public async Task<IEnumerable<Meeting>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Meetings.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Meeting>> GetAllByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default)
    {
       return await _context.Meetings.Where(m => m.TeamId == teamId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Meeting>> GetAllByStatusAsync(MeetingStatus status, CancellationToken cancellationToken = default)
    {
       return await _context.Meetings.Where(m => m.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Meeting>> GetAllByHostIdAsync(Guid hostId, CancellationToken cancellationToken = default)
    {
       return await _context.Meetings.Where(m => m.HostId == hostId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Meeting meeting, CancellationToken cancellationToken = default)
    {
        await _context.Meetings.AddAsync(meeting, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Meeting meeting, CancellationToken cancellationToken = default)
    {
        _context.Meetings.Update(meeting);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        await _context.Meetings
        .Where(m => m.Id == id)
        .ExecuteDeleteAsync(cancellationToken);
    }
}
