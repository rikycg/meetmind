using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetAllMeetings;

public class GetAllMeetingsQueryHandler : IRequestHandler<GetAllMeetingsQuery, IEnumerable<MeetingResponse>>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetAllMeetingsQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<IEnumerable<MeetingResponse>> Handle(GetAllMeetingsQuery request, CancellationToken cancellationToken = default)
    {
        var meetings = await _meetingRepository.GetAllAsync(cancellationToken);

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