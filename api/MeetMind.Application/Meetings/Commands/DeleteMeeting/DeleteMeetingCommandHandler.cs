using MediatR;
using MeetMind.Application.Interfaces;

namespace MeetMind.Application.Meetings.Commands.DeleteMeeting;

public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand>
{
    private readonly IMeetingRepository _meetingRepository;

    public DeleteMeetingCommandHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task Handle(DeleteMeetingCommand request, CancellationToken cancellationToken = default)
    {
        var meeting = await _meetingRepository.GetByIdAsync(request.Id, cancellationToken);

        if (meeting is null)
            throw new KeyNotFoundException($"Meeting with id '{request.Id}' was not found.");

        await _meetingRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
