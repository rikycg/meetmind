using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;
using MeetMind.Domain.Exceptions;
using MeetMind.Domain.Meetings;

namespace MeetMind.Application.Meetings.Commands.AddParticipant;

public class AddParticipantCommandHandler : IRequestHandler<AddParticipantCommand, ParticipantResponse>
{
    private readonly IParticipantRepository _participantRepository;

    public AddParticipantCommandHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task<ParticipantResponse> Handle(AddParticipantCommand request, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<ParticipantRole>(request.Role, true, out var role))
            throw new BadRequestException($"'{request.Role}' is not a valid participant role.");

        var existing = await _participantRepository.GetByMeetingIdAndUserIdAsync(request.MeetingId, request.UserId, cancellationToken);

        if (existing is not null)
            throw new ConflictException("User is already a participant in this meeting.");

        var participant = Participant.Create(request.UserId, request.MeetingId, role);

        await _participantRepository.AddAsync(participant, cancellationToken);

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
