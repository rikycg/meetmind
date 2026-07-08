using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Commands.LeaveParticipant;

public class LeaveParticipantCommandHandler : IRequestHandler<LeaveParticipantCommand, ParticipantResponse>
{
    private readonly IParticipantRepository _participantRepository;

    public LeaveParticipantCommandHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task<ParticipantResponse> Handle(LeaveParticipantCommand request, CancellationToken cancellationToken = default)
    {
        var participant = await _participantRepository.GetByIdAsync(request.Id, cancellationToken);

        if (participant is null)
            throw new KeyNotFoundException($"Participant with id '{request.Id}' was not found.");

        participant.LeftMeeting();

        await _participantRepository.UpdateAsync(participant, cancellationToken);

        return new ParticipantResponse(
            participant.Id,
            participant.UserId,
            participant.MeetingId,
            participant.Role.ToString(),
            participant.JoinedAt,
            participant.LeftAt
        );
    }
}
