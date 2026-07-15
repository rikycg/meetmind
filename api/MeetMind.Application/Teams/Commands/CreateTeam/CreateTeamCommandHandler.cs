using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;
using MeetMind.Domain.Exceptions;
using MeetMind.Domain.Teams;

namespace MeetMind.Application.Teams.Commands.CreateTeam;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, TeamResponse>
{
    private readonly ITeamRepository _teamRepository;

    public CreateTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<TeamResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken = default)
    {
        var exists = await _teamRepository.ExistsAsync(request.Name, cancellationToken);

        if (exists)
            throw new ConflictException($"A team with name '{request.Name}' already exists.");

        var team = Team.Create(request.Name, request.Description);

        await _teamRepository.AddAsync(team, cancellationToken);

        return new TeamResponse(
            team.Id,
            team.Name,
            team.Description,
            team.CreatedAt,
            team.UpdatedAt
        );
    }
}
