using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class Meeting : Entity {
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid HostId { get; private set; }
    public Guid? TeamId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? EndedAt { get; private set; }
    public MeetingStatus Status { get; private set; }
    
    private Meeting() : base() { }
    private Meeting(string title, string description, DateTime scheduledAt, Guid hostId, MeetingStatus status, Guid? teamId) {
        Title = title;
        Description = description;
        ScheduledAt = scheduledAt;
        HostId = hostId;
        Status = status;

        if (teamId.HasValue && teamId != Guid.Empty)
            TeamId = teamId;
    }

    public static Meeting Create (string title, string description, DateTime scheduledAt, Guid hostId, Guid? teamId) {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("title is empty.");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("description is empty.");

        string titleCleaned = title.Trim();
        string descriptionCleaned = description.Trim();
        MeetingStatus status = MeetingStatus.Scheduled;

        return new Meeting(titleCleaned, descriptionCleaned, scheduledAt, hostId, status, teamId);
    }

    public void Start() {
        if (Status != MeetingStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled meetings can be started.");
        
        Status = MeetingStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel() {
        if (Status != MeetingStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled meetings can be cancelled.");

        Status = MeetingStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete() {
        if (Status != MeetingStatus.InProgress)
            throw new InvalidOperationException("Only in progress meetings can be completed.");

        Status = MeetingStatus.Completed;
        EndedAt = DateTime.UtcNow;
    }
}