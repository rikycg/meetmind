using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class MeetingSummary : Entity {
    public Guid MeetingId { get; private set; }
    public string Summary { get; private set; }
    private readonly List<KeyDecision> _keyDecisions = new();
    public IReadOnlyList<KeyDecision> KeyDecisions => _keyDecisions.AsReadOnly();
    private readonly List<ActionItem> _actionItems = new();
    public IReadOnlyList<ActionItem> ActionItems => _actionItems.AsReadOnly();

    private MeetingSummary(): base() {}

    private MeetingSummary(Guid meetingId, string summary) {
        MeetingId = meetingId;
        Summary = summary;
    }

    public static MeetingSummary Create(Guid meetingId, string summary) {
        if (meetingId == Guid.Empty) 
            throw new ArgumentException("meetingId is empty.");

        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("summary is empty.");

        return new MeetingSummary(meetingId, summary);
    }

    public void AddKeyDecision(string content) {
        _keyDecisions.Add(KeyDecision.Create(Id, content));
    }

    public void AddActionItem(string title, string? description, Guid? assignedTo, DateTime? dueDate) {
        _actionItems.Add(ActionItem.Create(Id, title, description, assignedTo, dueDate));
    }
}
