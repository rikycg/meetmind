using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.CompleteMeeting;

public class CompleteMeetingCommandHandler : IRequestHandler<CompleteMeetingCommand, MeetingResponse>
{
    private readonly IMeetingRepository _meetingRepository;

    public CompleteMeetingCommandHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<MeetingResponse> Handle(CompleteMeetingCommand request, CancellationToken cancellationToken = default)
    {
        var meeting = await _meetingRepository.GetByIdAsync(request.Id, cancellationToken);

        if (meeting is null)
            throw new KeyNotFoundException($"Meeting with id '{request.Id}' was not found.");

        meeting.Complete();

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
