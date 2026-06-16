using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface ITranscriptRepository {
    Task<Transcript?> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transcript>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Transcript transcript, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
