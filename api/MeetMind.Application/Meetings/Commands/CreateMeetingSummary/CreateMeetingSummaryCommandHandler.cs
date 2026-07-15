using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Meetings;
using MeetMind.Domain.Exceptions;

namespace MeetMind.Application.Meetings.Commands.CreateMeetingSummary;

public class CreateMeetingSummaryCommandHandler : IRequestHandler<CreateMeetingSummaryCommand, MeetingSummaryResponse>
{
    private readonly IMeetingSummaryRepository _meetingSummaryRepository;

    public CreateMeetingSummaryCommandHandler(IMeetingSummaryRepository meetingSummaryRepository)
    {
        _meetingSummaryRepository = meetingSummaryRepository;
    }

    public async Task<MeetingSummaryResponse> Handle(CreateMeetingSummaryCommand request, CancellationToken cancellationToken = default)
    {
        var existing = await _meetingSummaryRepository.GetByMeetingIdAsync(request.MeetingId, cancellationToken);

        if (existing is not null)
            throw new ConflictException("A summary already exists for this meeting.");

        var summary = MeetingSummary.Create(request.MeetingId, request.Summary);

        await _meetingSummaryRepository.AddAsync(summary, cancellationToken);

        return new MeetingSummaryResponse(
            summary.Id,
            summary.MeetingId,
            summary.Summary,
            summary.CreatedAt,
            summary.UpdatedAt
        );
    }
}
