using MediatR;
using MeetMind.Domain.Exceptions;
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
            throw new NotFoundException($"Team with name '{request.Name}' was not found.");

        return new TeamResponse(
            team.Id,
            team.Name,
            team.Description,
            team.CreatedAt,
            team.UpdatedAt
        );
    }
}
