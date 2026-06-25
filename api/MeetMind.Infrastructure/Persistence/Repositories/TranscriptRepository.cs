using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class TranscriptRepository : ITranscriptRepository
{
    private readonly MeetMindDbContext _context;

    public TranscriptRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<Transcript?> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default)
    {
        return await _context.Transcripts
            .FirstOrDefaultAsync(t => t.MeetingId == meetingId, cancellationToken);
    }

    public async Task<IEnumerable<Transcript>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Transcripts.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Transcript transcript, CancellationToken cancellationToken = default)
    {
        await _context.Transcripts.AddAsync(transcript, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Transcripts
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
