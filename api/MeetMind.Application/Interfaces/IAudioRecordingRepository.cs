using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface IAudioRecordingRepository {
    Task<AudioRecording?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AudioRecording>> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AudioRecording>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(AudioRecording audioRecording, CancellationToken cancellationToken = default);
    Task UpdateAsync(AudioRecording audioRecording, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
