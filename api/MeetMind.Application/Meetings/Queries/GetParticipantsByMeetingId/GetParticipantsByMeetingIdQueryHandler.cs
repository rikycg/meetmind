using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Meetings.Common;

namespace MeetMind.Application.Meetings.Queries.GetParticipantsByMeetingId;

public class GetParticipantsByMeetingIdQueryHandler : IRequestHandler<GetParticipantsByMeetingIdQuery, IEnumerable<ParticipantResponse>>
{
    private readonly IParticipantRepository _participantRepository;

    public GetParticipantsByMeetingIdQueryHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task<IEnumerable<ParticipantResponse>> Handle(GetParticipantsByMeetingIdQuery request, CancellationToken cancellationToken = default)
    {
        var participants = await _participantRepository.GetAllByMeetingIdAsync(request.MeetingId, cancellationToken);

        return participants.Select(p => new ParticipantResponse(
            p.Id,
            p.UserId,
            p.MeetingId,
            p.Role.ToString(),
            p.JoinedAt,
            p.LeftAt
        ));
    }
}
