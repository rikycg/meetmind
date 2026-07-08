using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Commands.CreateKeyDecision;

public class CreateKeyDecisionCommandHandler : IRequestHandler<CreateKeyDecisionCommand, KeyDecisionResponse>
{
    private readonly IKeyDecisionRepository _keyDecisionRepository;

    public CreateKeyDecisionCommandHandler(IKeyDecisionRepository keyDecisionRepository)
    {
        _keyDecisionRepository = keyDecisionRepository;
    }

    public async Task<KeyDecisionResponse> Handle(CreateKeyDecisionCommand request, CancellationToken cancellationToken = default)
    {
        var keyDecision = KeyDecision.Create(request.SummaryId, request.Content);

        await _keyDecisionRepository.AddAsync(keyDecision, cancellationToken);

        return new KeyDecisionResponse(
            keyDecision.Id,
            keyDecision.SummaryId,
            keyDecision.Content,
            keyDecision.CreatedAt
        );
    }
}
