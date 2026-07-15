using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Exceptions;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Queries.GetMeetingsByStatus;

public class GetMeetingsByStatusQueryHandler : IRequestHandler<GetMeetingsByStatusQuery, IEnumerable<MeetingResponse>>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetMeetingsByStatusQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<IEnumerable<MeetingResponse>> Handle(GetMeetingsByStatusQuery request, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<MeetingStatus>(request.Status, true, out var status))
            throw new BadRequestException($"'{request.Status}' is not a valid meeting status.");

        var meetings = await _meetingRepository.GetAllByStatusAsync(status, cancellationToken);

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
