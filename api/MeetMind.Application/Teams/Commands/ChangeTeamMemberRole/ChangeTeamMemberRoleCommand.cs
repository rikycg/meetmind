using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Commands.ChangeTeamMemberRole;

public record ChangeTeamMemberRoleCommand(Guid Id, string Role) : IRequest<TeamMemberResponse>;
