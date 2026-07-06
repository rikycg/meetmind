using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CancelMeeting;

public class CancelMeetingCommandHandler : IRequestHandler<CancelMeetingCommand, MeetingResponse>
{
    private readonly IMeetingRepository _meetingRespository;

    public CancelMeetingCommandHandler(IMeetingRepository meetingRepository)
    {
        _meetingRespository = meetingRepository;
    }

    public async Task<MeetingResponse> Handle(CancelMeetingCommand request, CancellationToken cancellationToken = default)
    {
        var meeting = await _meetingRespository.GetByIdAsync(request.Id, cancellationToken);

        if (meeting is null) {
            throw new InvalidOperationException("The meeting doesn't exist");
        }

        meeting.Cancel();

        await _meetingRespository.UpdateAsync(meeting, cancellationToken);

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