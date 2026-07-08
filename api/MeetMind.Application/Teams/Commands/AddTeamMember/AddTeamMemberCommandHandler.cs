using MediatR;
using MeetMind.Application.Interfaces;
using MeetMind.Application.Teams.Common;
using MeetMind.Domain.Teams;

namespace MeetMind.Application.Teams.Commands.AddTeamMember;

public class AddTeamMemberCommandHandler : IRequestHandler<AddTeamMemberCommand, TeamMemberResponse>
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public AddTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<TeamMemberResponse> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<TeamMemberRole>(request.Role, true, out var role))
            throw new ArgumentException($"'{request.Role}' is not a valid team member role.");

        var teamMember = TeamMember.Create(request.UserId, request.TeamId, role);

        await _teamMemberRepository.AddToTeamAsync(teamMember, cancellationToken);

        return new TeamMemberResponse(
            teamMember.Id,
            teamMember.UserId,
            teamMember.TeamId,
            teamMember.Role.ToString(),
            teamMember.JoinedAt
        );
    }
}
