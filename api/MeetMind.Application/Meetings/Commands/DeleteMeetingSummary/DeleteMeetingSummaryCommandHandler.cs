using MediatR;
using MeetMind.Application.Interfaces;

namespace MeetMind.Application.Meetings.Commands.DeleteMeetingSummary;

public class DeleteMeetingSummaryCommandHandler : IRequestHandler<DeleteMeetingSummaryCommand>
{
    private readonly IMeetingSummaryRepository _meetingSummaryRepository;

    public DeleteMeetingSummaryCommandHandler(IMeetingSummaryRepository meetingSummaryRepository)
    {
        _meetingSummaryRepository = meetingSummaryRepository;
    }

    public async Task Handle(DeleteMeetingSummaryCommand request, CancellationToken cancellationToken = default)
    {
        await _meetingSummaryRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
