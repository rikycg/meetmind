using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface IKeyDecisionRepository {
    Task<KeyDecision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<KeyDecision>> GetAllBySummaryIdAsync(Guid summaryId, CancellationToken cancellationToken = default);
    Task AddAsync(KeyDecision keyDecision, CancellationToken cancellationToken = default);
    Task UpdateAsync(KeyDecision keyDecision, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);  
}