using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class TranscriptSegment : Entity {
    public Guid TranscriptId { get; private set; }
    public Guid SpeakerId { get; private set; }
    public string Text { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public double Confidence { get; private set; }

    private TranscriptSegment(): base () {}

    private TranscriptSegment(Guid transcriptId, Guid speakerId, string text, TimeSpan startTime, TimeSpan endTime, double confidence) {
        TranscriptId = transcriptId;
        SpeakerId = speakerId;
        Text = text;
        StartTime = startTime;
        EndTime = endTime;
        Confidence = confidence;
    }

    public static TranscriptSegment Create(Guid transcriptId, Guid speakerId, string text, TimeSpan startTime, TimeSpan endTime, double confidence) {
        if (transcriptId == Guid.Empty)
            throw new ArgumentException("transcriptId is empty");
        
        if (speakerId == Guid.Empty)
            throw new ArgumentException("speakerId is empty");

        if (string.IsNullOrWhiteSpace(text)) 
            throw new ArgumentException("text is empty");

        if (startTime >= endTime)
            throw new ArgumentException("startTime must be before endTime.");

        return new TranscriptSegment(transcriptId, speakerId, text, startTime, endTime, confidence);
    }

}