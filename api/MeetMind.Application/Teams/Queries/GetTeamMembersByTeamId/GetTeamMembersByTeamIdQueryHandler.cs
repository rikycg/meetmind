using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetTeamMembersByTeamId;

public class GetTeamMembersByTeamIdQueryHandler : IRequestHandler<GetTeamMembersByTeamIdQuery, IEnumerable<TeamMemberResponse>>
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public GetTeamMembersByTeamIdQueryHandler(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<IEnumerable<TeamMemberResponse>> Handle(GetTeamMembersByTeamIdQuery request, CancellationToken cancellationToken = default)
    {
        var members = await _teamMemberRepository.GetAllByTeamIdAsync(request.TeamId, cancellationToken);

        return members.Select(m => new TeamMemberResponse(
            m.Id,
            m.UserId,
            m.TeamId,
            m.Role.ToString(),
            m.JoinedAt
        ));
    }
}
