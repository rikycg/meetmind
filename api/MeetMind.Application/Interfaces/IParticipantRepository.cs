using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Interfaces;

public interface IParticipantRepository {
    Task<Participant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Participant>> GetAllByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default);
    Task<Participant?> GetByMeetingIdAndUserIdAsync(Guid meetingId, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Participant>> GetAllByMeetingIdAndRoleAsync(Guid meetingId, ParticipantRole role, CancellationToken cancellationToken = default);
    Task AddAsync(Participant participant, CancellationToken cancellationToken = default);
    Task UpdateAsync(Participant participant, CancellationToken cancellationToken = default);
    Task RemoveAsync(Guid participantId, CancellationToken cancellationToken = default);
}