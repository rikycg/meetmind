using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class TranscriptSegmentRepository : ITranscriptSegmentRepository
{
    private readonly MeetMindDbContext _context;

    public TranscriptSegmentRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<TranscriptSegment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TranscriptSegments.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<TranscriptSegment>> GetAllByTranscriptIdAsync(Guid transcriptId, CancellationToken cancellationToken = default)
    {
        return await _context.TranscriptSegments
            .Where(ts => ts.TranscriptId == transcriptId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TranscriptSegment transcriptSegment, CancellationToken cancellationToken = default)
    {
        await _context.TranscriptSegments.AddAsync(transcriptSegment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TranscriptSegment transcriptSegment, CancellationToken cancellationToken = default)
    {
        _context.TranscriptSegments.Update(transcriptSegment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.TranscriptSegments
            .Where(ts => ts.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DeleteByTranscriptIdAsync(Guid transcriptId, CancellationToken cancellationToken = default)
    {
        await _context.TranscriptSegments
            .Where(ts => ts.TranscriptId == transcriptId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
