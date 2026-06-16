using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface IMeetingSummaryRepository {
    Task<MeetingSummary?> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MeetingSummary>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(MeetingSummary meetingSummary, CancellationToken cancellationToken = default);
    Task UpdateAsync(MeetingSummary meetingSummary, CancellationToken  cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
