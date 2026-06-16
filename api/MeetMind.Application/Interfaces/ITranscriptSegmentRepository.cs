using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface ITranscriptSegmentRepository {
    Task<TranscriptSegment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TranscriptSegment>> GetAllByTranscriptIdAsync(Guid transcriptId, CancellationToken cancellationToken = default);
    Task AddAsync(TranscriptSegment transcriptSegment, CancellationToken cancellationToken = default);
    Task UpdateAsync(TranscriptSegment transcriptSegment, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteByTranscriptIdAsync(Guid transcriptId, CancellationToken cancellationToken = default);
}
