using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetKeyDecisionsBySummaryId;

public class GetKeyDecisionsBySummaryIdQueryHandler : IRequestHandler<GetKeyDecisionsBySummaryIdQuery, IEnumerable<KeyDecisionResponse>>
{
    private readonly IKeyDecisionRepository _keyDecisionRepository;

    public GetKeyDecisionsBySummaryIdQueryHandler(IKeyDecisionRepository keyDecisionRepository)
    {
        _keyDecisionRepository = keyDecisionRepository;
    }

    public async Task<IEnumerable<KeyDecisionResponse>> Handle(GetKeyDecisionsBySummaryIdQuery request, CancellationToken cancellationToken = default)
    {
        var decisions = await _keyDecisionRepository.GetAllBySummaryIdAsync(request.SummaryId, cancellationToken);

        return decisions.Select(d => new KeyDecisionResponse(
            d.Id,
            d.SummaryId,
            d.Content,
            d.CreatedAt
        ));
    }
}
