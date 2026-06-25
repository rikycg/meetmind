using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class AudioRecordingRepository : IAudioRecordingRepository {
    private readonly MeetMindDbContext _context;

    public AudioRecordingRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<AudioRecording?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.AudioRecordings.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<AudioRecording>> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default)
    {
        return await _context.AudioRecordings
            .Where(ar => ar.MeetingId == meetingId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AudioRecording>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.AudioRecordings.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AudioRecording audioRecording, CancellationToken cancellationToken = default)
    {
        await _context.AudioRecordings.AddAsync(audioRecording, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AudioRecording audioRecording, CancellationToken cancellationToken = default)
    {
        _context.AudioRecordings.Update(audioRecording);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.AudioRecordings.Where(ar => ar.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}