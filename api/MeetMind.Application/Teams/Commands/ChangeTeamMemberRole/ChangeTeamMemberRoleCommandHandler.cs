using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;
using MeetMind.Domain.Teams;

namespace MeetMind.Application.Teams.Commands.ChangeTeamMemberRole;

public class ChangeTeamMemberRoleCommandHandler : IRequestHandler<ChangeTeamMemberRoleCommand, TeamMemberResponse>
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public ChangeTeamMemberRoleCommandHandler(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<TeamMemberResponse> Handle(ChangeTeamMemberRoleCommand request, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<TeamMemberRole>(request.Role, true, out var role))
            throw new ArgumentException($"'{request.Role}' is not a valid team member role.");

        var member = await _teamMemberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (member is null)
            throw new KeyNotFoundException($"Team member with id '{request.Id}' was not found.");

        member.ChangeRole(role);

        return new TeamMemberResponse(
            member.Id,
            member.UserId,
            member.TeamId,
            member.Role.ToString(),
            member.JoinedAt
        );
    }
}
