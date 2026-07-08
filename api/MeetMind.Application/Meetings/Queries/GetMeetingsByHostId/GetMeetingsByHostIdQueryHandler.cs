using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingsByHostId;

public class GetMeetingsByHostIdQueryHandler : IRequestHandler<GetMeetingsByHostIdQuery, IEnumerable<MeetingResponse>>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetMeetingsByHostIdQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<IEnumerable<MeetingResponse>> Handle(GetMeetingsByHostIdQuery request, CancellationToken cancellationToken = default)
    {
        var meetings = await _meetingRepository.GetAllByHostIdAsync(request.HostId, cancellationToken);

        return meetings.Select(meeting => new MeetingResponse(
            meeting.Id,
            meeting.Title,
            meeting.Description,
            meeting.HostId,
            meeting.TeamId,
            meeting.ScheduledAt,
            meeting.StartedAt,
            meeting.EndedAt,
            meeting.Status.ToString()
        ));
    }
}
