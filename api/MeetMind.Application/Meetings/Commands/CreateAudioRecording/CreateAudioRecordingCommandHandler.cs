using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Commands.CreateAudioRecording;

public class CreateAudioRecordingCommandHandler : IRequestHandler<CreateAudioRecordingCommand, AudioRecordingResponse>
{
    private readonly IAudioRecordingRepository _audioRecordingRepository;

    public CreateAudioRecordingCommandHandler(IAudioRecordingRepository audioRecordingRepository)
    {
        _audioRecordingRepository = audioRecordingRepository;
    }

    public async Task<AudioRecordingResponse> Handle(CreateAudioRecordingCommand request, CancellationToken cancellationToken = default)
    {
        var recording = AudioRecording.Create(
            request.MeetingId,
            request.FileUrl,
            request.Duration,
            request.FileSize,
            request.Format
        );

        await _audioRecordingRepository.AddAsync(recording, cancellationToken);

        return new AudioRecordingResponse(
            recording.Id,
            recording.MeetingId,
            recording.FileUrl,
            recording.Duration,
            recording.FileSize,
            recording.Format,
            recording.CreatedAt
        );
    }
}
