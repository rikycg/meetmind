using MeetMind.Application.Interfaces;
using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly MeetMindDbContext _context;

    public ParticipantRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<Participant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Participants.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Participant>> GetAllByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default)
    {
        return await _context.Participants
            .Where(p => p.MeetingId == meetingId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Participant?> GetByMeetingIdAndUserIdAsync(Guid meetingId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Participants
            .FirstOrDefaultAsync(p => p.MeetingId == meetingId && p.UserId == userId, cancellationToken);
    }

    public async Task<IEnumerable<Participant>> GetAllByMeetingIdAndRoleAsync(Guid meetingId, ParticipantRole role, CancellationToken cancellationToken = default)
    {
        return await _context.Participants
            .Where(p => p.MeetingId == meetingId && p.Role == role)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Participant participant, CancellationToken cancellationToken = default)
    {
        await _context.Participants.AddAsync(participant, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Participant participant, CancellationToken cancellationToken = default)
    {
        _context.Participants.Update(participant);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Guid participantId, CancellationToken cancellationToken = default)
    {
        await _context.Participants
            .Where(p => p.Id == participantId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
