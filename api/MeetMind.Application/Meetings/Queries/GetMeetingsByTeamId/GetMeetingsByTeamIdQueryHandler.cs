using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingsByTeamId;

public class GetMeetingsByTeamIdQueryHandler : IRequestHandler<GetMeetingsByTeamIdQuery, IEnumerable<MeetingResponse>>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetMeetingsByTeamIdQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<IEnumerable<MeetingResponse>> Handle(GetMeetingsByTeamIdQuery request, CancellationToken cancellationToken = default)
    {
        var meetings = await _meetingRepository.GetAllByTeamIdAsync(request.TeamId, cancellationToken);

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
