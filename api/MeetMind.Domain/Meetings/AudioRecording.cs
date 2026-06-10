using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class AudioRecording : Entity {
    public Guid MeetingId { get; private set; }
    public string FileUrl { get; private set; }
    public int Duration { get; private set; }
    public long FileSize { get; private set; }
    public string Format { get; private set; }

    private AudioRecording(): base() {}

    private AudioRecording(Guid meetingId, string fileUrl, int duration, long fileSize, string format) {
        MeetingId = meetingId;
        FileUrl = fileUrl;
        Duration = duration;
        FileSize = fileSize;
        Format = format;
    }

    public static AudioRecording Create(Guid meetingId, string fileUrl, int duration, long fileSize, string format) {
        if (string.IsNullOrWhiteSpace(fileUrl))
            throw new ArgumentException("fileUrl is empty.");
        
        if (string.IsNullOrWhiteSpace(format))
            throw new ArgumentException("format is empty.");
        
        if (meetingId == Guid.Empty)
            throw new ArgumentException("meetingId is empty.");

        if (duration <= 0)
            throw new ArgumentException("duration must be greater than zero.");

        if (fileSize <= 0)
            throw new ArgumentException("fileSize must be greater than zero.");

        string fileUrlCleaned = fileUrl.Trim();
        string formatCleaned = format.Trim();
        
        return new AudioRecording(meetingId, fileUrlCleaned, duration, fileSize, formatCleaned);
    }
}