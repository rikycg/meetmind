using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Commands.CreateMeeting;

public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, MeetingResponse>
{
    private readonly IMeetingRepository _meetingRepository;

    public CreateMeetingCommandHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<MeetingResponse> Handle(CreateMeetingCommand request, CancellationToken cancellationToken = default)
    {
        var meeting = Meeting.Create(
            request.Title,
            request.Description,
            request.ScheduledAt,
            request.HostId,
            request.TeamId
        );

        await _meetingRepository.AddAsync(meeting, cancellationToken);

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