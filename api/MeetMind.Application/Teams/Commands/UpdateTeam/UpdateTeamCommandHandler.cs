using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;
using MeetMind.Domain.Exceptions;

namespace MeetMind.Application.Teams.Commands.UpdateTeam;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, TeamResponse>
{
    private readonly ITeamRepository _teamRepository;

    public UpdateTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<TeamResponse> Handle(UpdateTeamCommand request, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);

        if (team is null)
            throw new NotFoundException($"Team with id '{request.Id}' was not found.");

        team.UpdateInfo(request.Name, request.Description);

        await _teamRepository.UpdateAsync(team, cancellationToken);

        return new TeamResponse(
            team.Id,
            team.Name,
            team.Description,
            team.CreatedAt,
            team.UpdatedAt
        );
    }
}
