using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.StartMeeting;

public class StartMeetingCommandHandler : IRequestHandler<StartMeetingCommand, MeetingResponse>
{
    private readonly IMeetingRepository _meetingRepository;

    public StartMeetingCommandHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<MeetingResponse> Handle(StartMeetingCommand request, CancellationToken cancellationToken = default)
    {
        var meeting = await _meetingRepository.GetByIdAsync(request.Id, cancellationToken);

        if (meeting is null)
            throw new KeyNotFoundException($"Meeting with id '{request.Id}' was not found.");

        meeting.Start();

        await _meetingRepository.UpdateAsync(meeting, cancellationToken);

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
