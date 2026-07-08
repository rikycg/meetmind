using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetMeetingByTitle;

public class GetMeetingByTitleQueryHandler : IRequestHandler<GetMeetingByTitleQuery, MeetingResponse?>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetMeetingByTitleQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<MeetingResponse?> Handle(GetMeetingByTitleQuery request, CancellationToken cancellationToken = default)
    {
        var meeting = await _meetingRepository.GetByTitleAsync(request.Title, cancellationToken);

        if (meeting is null)
            return null;

        return new MeetingResponse(
            meeting.Id,
            meeting.Title,
            meeting.Description,
            meeting.HostId,
            meeting.TeamId,
            meeting.ScheduledAt,
            meeting.StartedAt,
            meeting.EndedAt,
            meeting.Status.ToString()
        );
    }
}
