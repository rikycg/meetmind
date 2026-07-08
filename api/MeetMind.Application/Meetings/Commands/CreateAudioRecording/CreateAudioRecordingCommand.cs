using MediatR;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CreateAudioRecording;

public record CreateAudioRecordingCommand(
    Guid MeetingId,
    string FileUrl,
    int Duration,
    long FileSize,
    string Format
) : IRequest<AudioRecordingResponse>;
