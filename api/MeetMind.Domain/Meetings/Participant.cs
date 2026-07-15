using MeetMind.Domain.Exceptions;
using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class Participant : Entity {
    public Guid UserId { get; private set; }
    public Guid MeetingId { get; private set; }
    public ParticipantRole Role { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public DateTime? LeftAt { get; private set; }

    private Participant() : base () {}

    private Participant(Guid userId, Guid meetingId, ParticipantRole role) {
        JoinedAt = DateTime.UtcNow;
        UserId = userId;
        MeetingId = meetingId;
        Role = role;
    }

    public static Participant Create(Guid userId, Guid meetingId, ParticipantRole role) {
        if (userId == Guid.Empty)
            throw new ArgumentException("userId is empty.");
        
        if (meetingId == Guid.Empty)
            throw new ArgumentException("meetingId is empty.");

        return new Participant(userId, meetingId, role);
    }

    public void LeftMeeting() {
        if (LeftAt.HasValue)
            throw new ConflictException("Participant already left the meeting.");

        LeftAt = DateTime.UtcNow;
    }

    public void ChangeRole(ParticipantRole role) {
        Role = role;
        UpdatedAt = DateTime.UtcNow;
    }
}