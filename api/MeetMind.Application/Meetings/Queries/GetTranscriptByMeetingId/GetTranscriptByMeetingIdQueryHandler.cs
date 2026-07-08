using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetTranscriptByMeetingId;

public class GetTranscriptByMeetingIdQueryHandler : IRequestHandler<GetTranscriptByMeetingIdQuery, TranscriptResponse?>
{
    private readonly ITranscriptRepository _transcriptRepository;

    public GetTranscriptByMeetingIdQueryHandler(ITranscriptRepository transcriptRepository)
    {
        _transcriptRepository = transcriptRepository;
    }

    public async Task<TranscriptResponse?> Handle(GetTranscriptByMeetingIdQuery request, CancellationToken cancellationToken = default)
    {
        var transcript = await _transcriptRepository.GetByMeetingIdAsync(request.MeetingId, cancellationToken);

        if (transcript is null)
            return null;

        return new TranscriptResponse(
            transcript.Id,
            transcript.MeetingId,
            transcript.Language.ToString(),
            transcript.Content,
            transcript.CreatedAt
        );
    }
}
