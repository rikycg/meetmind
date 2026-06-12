using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class KeyDecision: Entity {
    public Guid SummaryId { get; private set; }
    public string Content { get; private set; }

    private KeyDecision(): base() {}

    private KeyDecision(Guid summaryId, string content) {
        SummaryId = summaryId;
        Content = content;
    }

    public static KeyDecision Create(Guid summaryId, string content) {
        if (summaryId == Guid.Empty) 
            throw new ArgumentException("summaryId is empty.");

        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("content is empty.");

        return new KeyDecision(summaryId, content);
    }
}
