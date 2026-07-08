using MediatR;
using MeetMind.Application.Interfaces;

namespace MeetMind.Application.Meetings.Commands.DeleteTranscript;

public class DeleteTranscriptCommandHandler : IRequestHandler<DeleteTranscriptCommand>
{
    private readonly ITranscriptRepository _transcriptRepository;

    public DeleteTranscriptCommandHandler(ITranscriptRepository transcriptRepository)
    {
        _transcriptRepository = transcriptRepository;
    }

    public async Task Handle(DeleteTranscriptCommand request, CancellationToken cancellationToken = default)
    {
        await _transcriptRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
