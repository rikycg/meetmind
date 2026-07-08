using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetTeamById;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamResponse?>
{
    private readonly ITeamRepository _teamRepository;

    public GetTeamByIdQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<TeamResponse?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);

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
