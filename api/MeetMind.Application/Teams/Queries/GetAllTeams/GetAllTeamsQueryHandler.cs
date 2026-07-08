using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetAllTeams;

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, IEnumerable<TeamResponse>>
{
    private readonly ITeamRepository _teamRepository;

    public GetAllTeamsQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<IEnumerable<TeamResponse>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken = default)
    {
        var teams = await _teamRepository.GetAllAsync(cancellationToken);

        return teams.Select(team => new TeamResponse(
            team.Id,
            team.Name,
            team.Description,
            team.CreatedAt,
            team.UpdatedAt
        ));
    }
}
