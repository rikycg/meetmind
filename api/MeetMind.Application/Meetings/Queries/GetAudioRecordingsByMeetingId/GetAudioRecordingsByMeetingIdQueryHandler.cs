using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetAudioRecordingsByMeetingId;

public class GetAudioRecordingsByMeetingIdQueryHandler : IRequestHandler<GetAudioRecordingsByMeetingIdQuery, IEnumerable<AudioRecordingResponse>>
{
    private readonly IAudioRecordingRepository _audioRecordingRepository;

    public GetAudioRecordingsByMeetingIdQueryHandler(IAudioRecordingRepository audioRecordingRepository)
    {
        _audioRecordingRepository = audioRecordingRepository;
    }

    public async Task<IEnumerable<AudioRecordingResponse>> Handle(GetAudioRecordingsByMeetingIdQuery request, CancellationToken cancellationToken = default)
    {
        var recordings = await _audioRecordingRepository.GetByMeetingIdAsync(request.MeetingId, cancellationToken);

        return recordings.Select(r => new AudioRecordingResponse(
            r.Id,
            r.MeetingId,
            r.FileUrl,
            r.Duration,
            r.FileSize,
            r.Format,
            r.CreatedAt
        ));
    }
}
