using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetTeamByName;

public class GetTeamByNameQueryHandler : IRequestHandler<GetTeamByNameQuery, TeamResponse?>
{
    private readonly ITeamRepository _teamRepository;

    public GetTeamByNameQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<TeamResponse?> Handle(GetTeamByNameQuery request, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByNameAsync(request.Name, cancellationToken);

        if (team is null)
            return null;

        return new TeamResponse(
            team.Id,
            team.Name,
            team.Description,
            team.CreatedAt,
            team.UpdatedAt
        );
    }
}
