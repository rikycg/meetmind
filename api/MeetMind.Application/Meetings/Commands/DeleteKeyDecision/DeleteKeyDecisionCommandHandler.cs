using MediatR;
using MeetMind.Application.Interfaces;

namespace MeetMind.Application.Meetings.Commands.DeleteKeyDecision;

public class DeleteKeyDecisionCommandHandler : IRequestHandler<DeleteKeyDecisionCommand>
{
    private readonly IKeyDecisionRepository _keyDecisionRepository;

    public DeleteKeyDecisionCommandHandler(IKeyDecisionRepository keyDecisionRepository)
    {
        _keyDecisionRepository = keyDecisionRepository;
    }

    public async Task Handle(DeleteKeyDecisionCommand request, CancellationToken cancellationToken = default)
    {
        await _keyDecisionRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
