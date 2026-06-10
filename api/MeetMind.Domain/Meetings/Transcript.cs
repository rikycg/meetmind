using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class Transcript : Entity {
    public Guid MeetingId { get; private set; }
    public TranscriptLanguage Language { get; private set; }
    public string Content { get; private set; }
    
    private Transcript(): base() {}

    private Transcript(Guid meetingId, TranscriptLanguage language, string content) {
        MeetingId = meetingId;
        Language = language;
        Content = content;
    }

    public static Transcript Create(Guid meetingId, TranscriptLanguage language, string content) {
        if (meetingId == Guid.Empty) 
            throw new ArgumentException("meetingId is empty.");

        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("content is empty.");

        return new Transcript(meetingId, language, content);
    }
}