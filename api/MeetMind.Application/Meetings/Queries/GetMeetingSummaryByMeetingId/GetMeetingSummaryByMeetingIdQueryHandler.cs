using MediatR;
using MeetMind.Domain.Exceptions;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingSummaryByMeetingId;

public class GetMeetingSummaryByMeetingIdQueryHandler : IRequestHandler<GetMeetingSummaryByMeetingIdQuery, MeetingSummaryResponse?>
{
    private readonly IMeetingSummaryRepository _meetingSummaryRepository;

    public GetMeetingSummaryByMeetingIdQueryHandler(IMeetingSummaryRepository meetingSummaryRepository)
    {
        _meetingSummaryRepository = meetingSummaryRepository;
    }

    public async Task<MeetingSummaryResponse?> Handle(GetMeetingSummaryByMeetingIdQuery request, CancellationToken cancellationToken = default)
    {
        var summary = await _meetingSummaryRepository.GetByMeetingIdAsync(request.MeetingId, cancellationToken);

        if (summary is null)
            throw new NotFoundException($"Summary with meeting id '{request.MeetingId}' was not found.");

        return new MeetingSummaryResponse(
            summary.Id,
            summary.MeetingId,
            summary.Summary,
            summary.CreatedAt,
            summary.UpdatedAt
        );
    }
}
