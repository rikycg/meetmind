using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Exceptions;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Commands.CreateTranscript;

public class CreateTranscriptCommandHandler : IRequestHandler<CreateTranscriptCommand, TranscriptResponse>
{
    private readonly ITranscriptRepository _transcriptRepository;

    public CreateTranscriptCommandHandler(ITranscriptRepository transcriptRepository)
    {
        _transcriptRepository = transcriptRepository;
    }

    public async Task<TranscriptResponse> Handle(CreateTranscriptCommand request, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<TranscriptLanguage>(request.Language, true, out var language))
            throw new BadRequestException($"'{request.Language}' is not a valid language.");

        var existing = await _transcriptRepository.GetByMeetingIdAsync(request.MeetingId, cancellationToken);

        if (existing is not null)
            throw new ConflictException("A transcript already exists for this meeting.");

        var transcript = Transcript.Create(request.MeetingId, language, request.Content);

        await _transcriptRepository.AddAsync(transcript, cancellationToken);

        return new TranscriptResponse(
            transcript.Id,
            transcript.MeetingId,
            transcript.Language.ToString(),
            transcript.Content,
            transcript.CreatedAt
        );
    }
}
