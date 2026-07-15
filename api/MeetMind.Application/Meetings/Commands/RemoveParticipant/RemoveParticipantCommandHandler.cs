using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Domain.Exceptions;

namespace MeetMind.Application.Meetings.Commands.RemoveParticipant;

public class RemoveParticipantCommandHandler : IRequestHandler<RemoveParticipantCommand>
{
    private readonly IParticipantRepository _participantRepository;

    public RemoveParticipantCommandHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task Handle(RemoveParticipantCommand request, CancellationToken cancellationToken = default)
    {
        var participant = await _participantRepository.GetByIdAsync(request.Id, cancellationToken);

        if (participant is null)
            throw new NotFoundException($"Participant with id '{request.Id}' was not found.");

        await _participantRepository.RemoveAsync(request.Id, cancellationToken);
    }
}
