using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Domain.Exceptions;

namespace MeetMind.Application.Meetings.Commands.DeleteAudioRecording;

public class DeleteAudioRecordingCommandHandler : IRequestHandler<DeleteAudioRecordingCommand>
{
    private readonly IAudioRecordingRepository _audioRecordingRepository;

    public DeleteAudioRecordingCommandHandler(IAudioRecordingRepository audioRecordingRepository)
    {
        _audioRecordingRepository = audioRecordingRepository;
    }

    public async Task Handle(DeleteAudioRecordingCommand request, CancellationToken cancellationToken = default)
    {
        var recording = await _audioRecordingRepository.GetByIdAsync(request.Id, cancellationToken);

        if (recording is null)
            throw new NotFoundException($"Audio recording with id '{request.Id}' was not found.");

        await _audioRecordingRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
