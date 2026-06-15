using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface IMeetingRepository {
    Task<Meeting?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Meeting?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);
    Task<IEnumerable<Meeting>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Meeting>> GetAllByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Meeting>> GetAllByStatusAsync(MeetingStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<Meeting>> GetAllByHostIdAsync(Guid hostId, CancellationToken  cancellationToken = default);
    Task AddAsync(Meeting meeting, CancellationToken cancellationToken = default);
    Task UpdateAsync(Meeting meeting, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
