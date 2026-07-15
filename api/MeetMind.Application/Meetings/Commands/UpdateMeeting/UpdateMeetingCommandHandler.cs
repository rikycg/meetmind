using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Exceptions;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Commands.UpdateMeeting;

public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, MeetingResponse>
{
    private readonly IMeetingRepository _meetingRepository;

    public UpdateMeetingCommandHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<MeetingResponse> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken = default)
    {
        var meeting = await _meetingRepository.GetByIdAsync(request.Id, cancellationToken);

        if (meeting is null) {
            throw new NotFoundException("The meeting to update was not found");
        }

        meeting.UpdateTitle(request.Title);
        meeting.UpdateDescription(request.Description);
        
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